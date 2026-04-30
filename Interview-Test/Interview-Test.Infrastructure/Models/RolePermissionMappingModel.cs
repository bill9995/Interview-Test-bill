using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview_Test.Models;

[Table("RolePermissionMappingTb")]
public class RolePermissionMappingModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RolePermissionMappingId { get; set; }
    [ForeignKey("RoleId")]
    public RoleModel Role { get; set; }
    [ForeignKey("PermissionId")]
    public PermissionModel Permission { get; set; }
}
