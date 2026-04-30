using Interview_Test.Models;

namespace Interview_Test.Repositories;

public static class Data
{
    public static List<UserModel> Users =>
    [
        new UserModel
        {
            Id = Guid.Parse("F90810B6-E017-431A-9DAE-A4BA7F9BC865"),
            UserId = "user02",
            Username = "Bob.M.Jackson",
            UserProfile = new UserProfileModel
            {
                FirstName = "Bob",
                LastName = "Jackson",
                Age = 28
            },
            UserRoleMappings = new List<UserRoleMappingModel>
            {
                new()
                {
                    User = new UserModel
                    {
                        Id = Guid.Parse("F90810B6-E017-431A-9DAE-A4BA7F9BC865"),
                        UserId = "user02",
                        Username = "Bob.M.Jackson",
                        UserProfile = new UserProfileModel
                        {
                            FirstName = "Bob",
                            LastName = "Jackson",
                            Age = 28
                        }
                    },
                    Role = new()
                    {
                        RoleId = 3,
                        RoleName = "document operation",
                        RolePermissionMappings = new List<RolePermissionMappingModel>
                        {
                            new()
                            {
                                Permission = new() { Permission = "3-01-printing-label" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "2-04-packing-report" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-04-picking-report" }
                            }
                        }
                    }
                }
            }
        },
        new UserModel
        {
            Id = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
            UserId = "user01",
            Username = "John.D.Smith",
            UserProfile = new UserProfileModel
            {
                FirstName = "John",
                LastName = "Smith",
                Age = null
            },
            UserRoleMappings = new List<UserRoleMappingModel>
            {
                new()
                {
                    User = new UserModel
                    {
                        Id = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
                        UserId = "user01",
                        Username = "John.D.Smith",
                        UserProfile = new UserProfileModel
                        {
                            FirstName = "John",
                            LastName = "Smith",
                            Age = null
                        }
                    },
                    Role = new()
                    {
                        RoleId = 1,
                        RoleName = "pick operation",
                        RolePermissionMappings = new List<RolePermissionMappingModel>
                        {
                            new()
                            {
                                Permission = new() { Permission = "3-01-printing-label" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-04-picking-report" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-01-picking-info" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-03-picking-confirm" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-02-picking-start" }
                            }
                        }
                    }
                },
                new()
                {
                    User = new UserModel
                    {
                        Id = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
                        UserId = "user01",
                        Username = "John.D.Smith",
                        UserProfile = new UserProfileModel
                        {
                            FirstName = "John",
                            LastName = "Smith",
                            Age = null
                        }
                    },
                    Role = new()
                    {
                        RoleId = 2,
                        RoleName = "pack operation",
                        RolePermissionMappings = new List<RolePermissionMappingModel>
                        {
                            new()
                            {
                                Permission = new() { Permission = "2-03-packing-confirm" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "2-04-packing-report" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-04-picking-report" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "2-02-packing-start" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "3-01-printing-label" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "2-01-packing-info" }
                            }
                        }
                    }
                },
                new()
                {
                    User = new UserModel
                    {
                        Id = Guid.Parse("02CE43A4-A378-4B30-B52E-227EFA6B696E"),
                        UserId = "user01",
                        Username = "John.D.Smith",
                        UserProfile = new UserProfileModel
                        {
                            FirstName = "John",
                            LastName = "Smith",
                            Age = null
                        }
                    },
                    Role = new()
                    {
                        RoleId = 3,
                        RoleName = "document operation",
                        RolePermissionMappings = new List<RolePermissionMappingModel>
                        {
                            new()
                            {
                                Permission = new() { Permission = "3-01-printing-label" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "2-04-packing-report" }
                            },
                            new()
                            {
                                Permission = new() { Permission = "1-04-picking-report" }
                            }
                        }
                    }
                }
            }
        }
    ];
}