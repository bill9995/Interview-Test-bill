using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Interview_Test.Models;

[Table("RoleTb")]
public class RoleModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public int RoleId { get; set; }
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string RoleName { get; set; }
    public ICollection<RolePermissionMappingModel> RolePermissionMappings { get; set; }
    public ICollection<UserRoleMappingModel> UserRoleMappings { get; set; }
}