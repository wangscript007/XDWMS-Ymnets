//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Apps.Models;
using System;
using System.ComponentModel.DataAnnotations;
namespace Apps.Models.WMS
{

	public partial class WMS_InvInfoModel:Virtual_WMS_InvInfoModel
	{
		
	}
	public class Virtual_WMS_InvInfoModel
	{
		[Display(Name = "库房ID")]
		public virtual int Id { get; set; }
		[Display(Name = "库房编码")]
		public virtual string InvCode { get; set; }
		[Display(Name = "库房名称")]
		public virtual string InvName { get; set; }
		[Display(Name = "说明")]
		public virtual string Remark { get; set; }
		[Display(Name = "状态")]
		public virtual string Status { get; set; }
		[Display(Name = "创建人")]
		public virtual string CreatePerson { get; set; }
		[Display(Name = "创建时间")]
		public virtual Nullable<System.DateTime> CreateTime { get; set; }
		[Display(Name = "修改人")]
		public virtual string ModifyPerson { get; set; }
		[Display(Name = "修改时间")]
		public virtual Nullable<System.DateTime> ModifyTime { get; set; }
		[Display(Name = "未设置")]
		public virtual Nullable<bool> IsDefault { get; set; }
		}
}
