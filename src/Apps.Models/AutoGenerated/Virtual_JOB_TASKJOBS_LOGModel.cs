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
namespace Apps.Models.JOB
{

	public partial class JOB_TASKJOBS_LOGModel:Virtual_JOB_TASKJOBS_LOGModel
	{
		
	}
	public class Virtual_JOB_TASKJOBS_LOGModel
	{
		[Display(Name = "ID")]
		public virtual int itemID { get; set; }
		[Display(Name = "ID")]
		public virtual string sno { get; set; }
		[Display(Name = "任务名称")]
		public virtual string taskName { get; set; }
		[Display(Name = "未设置")]
		public virtual string Id { get; set; }
		[Display(Name = "执行日期")]
		public virtual Nullable<System.DateTime> executeDt { get; set; }
		[Display(Name = "执行步骤")]
		public virtual string executeStep { get; set; }
		[Display(Name = "执行结果")]
		public virtual string result { get; set; }
		}
}
