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

    public dynamic GetUserById(string id)
    {
        var userEntity = _context.UserTb
            .Where(w => w.UserId == id)
            .Select(s => new
            {
                id = s.Id,
                userId = s.UserId,
                username = s.Username,
                firstName = s.UserProfile.FirstName,
                lastName = s.UserProfile.LastName,
                age = s.UserProfile.Age,
                roles = s.UserRoleMappings.Select(s2 => new
                {
                    roleId = s2.Role.RoleId,
                    roleName = s2.Role.RoleName
                }).OrderBy(o => o.roleId).ToList(),
                rawPermissions = s.UserRoleMappings
                    .SelectMany(s2 => s2.Role.RolePermissionMappings.Select(s => s.Permission.Permission))
                    .ToList()
            })
            .FirstOrDefault();

        if (userEntity == null) return null;

        return new 
        {
            userEntity.id,
            userEntity.userId,
            userEntity.username,
            userEntity.firstName,
            userEntity.lastName,
            userEntity.age,
            userEntity.roles,
            permissions = userEntity.rawPermissions.Distinct().OrderBy(o => o).ToList()
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