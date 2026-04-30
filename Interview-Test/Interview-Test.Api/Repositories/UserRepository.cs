using Interview_Test.Models;
using Interview_Test.Repositories.Interfaces;
using Interview_Test.Infrastructure;

namespace Interview_Test.Repositories;

public class UserRepository : IUserRepository
{
    private readonly InterviewTestDbContext _context;

    public UserRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public IEnumerable<dynamic> GetUsers()
    {
        var users = _context.UserTb
            .Select(s => new
            {
                id = s.Id,
                userId = s.UserId,
                username = s.Username,
                name = s.UserProfile != null ? s.UserProfile.FirstName + " " + s.UserProfile.LastName : "",
                age = s.UserProfile != null ? (int?)s.UserProfile.Age : null,
                rolesCount = s.UserRoleMappings.Count(),
                rawPermissions = s.UserRoleMappings
                    .SelectMany(s2 => s2.Role.RolePermissionMappings.Select(p => p.Permission.Permission))
                    .ToList()
            })
            .ToList();

        return users.Select(s => new
        {
            s.id,
            s.userId,
            s.username,
            s.name,
            s.age,
            s.rolesCount,
            permissionsCount = s.rawPermissions.Distinct().Count()
        });
    }

    public dynamic GetUserById(string id)
    {
        var user = _context.UserTb
            .Where(w => w.Id.ToString() == id)
            .Select(s => new
            {
                s.Id,
                s.UserId,
                s.Username,
                s.UserProfile.FirstName,
                s.UserProfile.LastName,
                s.UserProfile.Age,
                Roles = s.UserRoleMappings.Select(s2 => new
                {
                    roleId = s2.Role.RoleId,
                    roleName = s2.Role.RoleName
                }).OrderBy(o => o.roleId).ToList(),
                Permissions = s.UserRoleMappings
                    .SelectMany(s2 => s2.Role.RolePermissionMappings.Select(s => s.Permission.Permission))
                    .ToList()
            })
            .FirstOrDefault();
        if (user == null) return null;
        return new 
        {
            user.Id,
            user.UserId,
            user.Username,
            user.FirstName,
            user.LastName,
            user.Age,
            user.Roles,
            Permissions = user.Permissions.Distinct().OrderBy(o => o).ToList()
        };
    }

    public int CreateUser()
    {
        //handeld exist user
        var userIds = _context.UserTb.Select(s => s.UserId).ToList();
        var newUsers = Data.Users.Where(w => !userIds.Contains(w.UserId)).ToList();
        if(newUsers.Count() == 0) return 0;
        
        //handeld exist role
        var roles = _context.RoleTb.ToList();
        var roleIdes = roles.Select(s => s.RoleId).ToList();
        var newRoles = newUsers
            .Where(w => w.UserRoleMappings != null)
            .SelectMany(s => 
                s.UserRoleMappings
                .Where(w => !roleIdes.Contains(w.Role.RoleId))
                .Select(s2 => new RoleModel()
                {
                    RoleId = s2.Role.RoleId,
                    RoleName = s2.Role.RoleName,
                    RolePermissionMappings = s2.Role.RolePermissionMappings
                })
            )
            .GroupBy(g => g.RoleId)
            .Select(g => g.First())
            .ToList();
        if (newRoles.Any()) roles.AddRange(newRoles);

        //handeld exist permission
        var permissions = _context.PermissionTb.ToList();
        var permissionNames = permissions.Select(s => s.Permission).ToList();
        var newPermissions = newRoles
            .Where(r => r.RolePermissionMappings != null)
            .SelectMany(r => r.RolePermissionMappings.Select(rpm => rpm.Permission))
            .Where(p => p != null && !permissionNames.Contains(p.Permission))
            .GroupBy(g => g.Permission)
            .Select(g => new PermissionModel { Permission = g.Key })
            .ToList();
        if (newPermissions.Any()) permissions.AddRange(newPermissions);

        //add new user
        foreach (var user in newUsers)
        {
            if (user.UserRoleMappings != null)
            {
                foreach (var mapping in user.UserRoleMappings)
                {
                    mapping.User = user;
                    if (mapping.Role != null)
                    {
                        var role = roles.Find(f => f.RoleId == mapping.Role.RoleId);
                        mapping.Role = role;

                        if (role != null && role.RolePermissionMappings != null)
                        {
                            foreach (var rpm in role.RolePermissionMappings)
                            {
                                rpm.Role = role;
                                if (rpm.Permission != null)
                                {
                                    var perm = permissions.Find(f => f.Permission == rpm.Permission.Permission);
                                    rpm.Permission = perm;
                                }
                            }
                        }
                    }
                }
            }
            _context.UserTb.Add(user);
        }
        return _context.SaveChanges();
    }
}