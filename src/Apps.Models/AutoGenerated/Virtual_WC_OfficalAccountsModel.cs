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
namespace Apps.Models.WC
{

	public partial class WC_OfficalAccountsModel:Virtual_WC_OfficalAccountsModel
	{
		
	}
	public class Virtual_WC_OfficalAccountsModel
	{
		[Display(Name = "GUID主键")]
		public virtual string Id { get; set; }
		[Display(Name = "公众号ID")]
		public virtual string OfficalId { get; set; }
		[Display(Name = "名称")]
		public virtual string OfficalName { get; set; }
		[Display(Name = "代码")]
		public virtual string OfficalCode { get; set; }
		[Display(Name = "照片")]
		public virtual string OfficalPhoto { get; set; }
		[Display(Name = "Key")]
		public virtual string OfficalKey { get; set; }
		[Display(Name = "中转URL")]
		public virtual string ApiUrl { get; set; }
		[Display(Name = "Token")]
		public virtual string Token { get; set; }
		[Display(Name = "AppID")]
		public virtual string AppId { get; set; }
		[Display(Name = "AppSecret")]
		public virtual string AppSecret { get; set; }
		[Display(Name = "获取得Token")]
		public virtual string AccessToken { get; set; }
		[Display(Name = "说明")]
		public virtual string Remark { get; set; }
		[Display(Name = "是否启用")]
		public virtual bool Enable { get; set; }
		[Display(Name = "当前操作号")]
		public virtual bool IsDefault { get; set; }
		[Display(Name = "类别")]
		public virtual int Category { get; set; }
		[Display(Name = "创建时间")]
		public virtual System.DateTime CreateTime { get; set; }
		[Display(Name = "创建人")]
		public virtual string CreateBy { get; set; }
		[Display(Name = "修改时间")]
		public virtual System.DateTime ModifyTime { get; set; }
		[Display(Name = "修改人")]
		public virtual string ModifyBy { get; set; }
		}
}
