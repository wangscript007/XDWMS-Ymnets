//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Apps.Models;
using Apps.Common;
using Unity.Attributes;
using System.Transactions;
using Apps.BLL.Core;
using Apps.Locale;
using LinqToExcel;
using System.IO;
using System.Text;
using Apps.IDAL.Sys;
using Apps.Models.Sys;
using Apps.IBLL.Sys;
namespace Apps.BLL.Sys
{
	public partial class SysLogBLL: Virtual_SysLogBLL,ISysLogBLL
	{
        

	}
	public class Virtual_SysLogBLL
	{
        [Dependency]
        public ISysLogRepository m_Rep { get; set; }

		public virtual List<SysLogModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysLog> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Operator.Contains(queryStr)
								|| a.Message.Contains(queryStr)
								|| a.Result.Contains(queryStr)
								|| a.Type.Contains(queryStr)
								|| a.Module.Contains(queryStr)
								
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

		public virtual List<SysLogModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<SysLogModel>();
		}
		
		public virtual List<SysLogModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<SysLogModel>();
		}

        public virtual List<SysLogModel> CreateModelList(ref IQueryable<SysLog> queryData)
        {

            List<SysLogModel> modelList = (from r in queryData
                                              select new SysLogModel
                                              {
													Id = r.Id,
													Operator = r.Operator,
													Message = r.Message,
													Result = r.Result,
													Type = r.Type,
													Module = r.Module,
													CreateTime = r.CreateTime,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysLogModel model)
        {
            try
            {
                SysLog entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysLog();
               				entity.Id = model.Id;
				entity.Operator = model.Operator;
				entity.Message = model.Message;
				entity.Result = model.Result;
				entity.Type = model.Type;
				entity.Module = model.Module;
				entity.CreateTime = model.CreateTime;
  

                if (m_Rep.Create(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }



         public virtual bool Delete(ref ValidationErrors errors, object id)
        {
            try
            {
                if (m_Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public virtual bool Delete(ref ValidationErrors errors, object[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (m_Rep.Delete(deleteCollection) == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

		
       

        public virtual bool Edit(ref ValidationErrors errors, SysLogModel model)
        {
            try
            {
                SysLog entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.Operator = model.Operator;
				entity.Message = model.Message;
				entity.Result = model.Result;
				entity.Type = model.Type;
				entity.Module = model.Module;
				entity.CreateTime = model.CreateTime;
 


                if (m_Rep.Edit(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.NoDataChange);
                    return false;
                }

            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

      

        public virtual SysLogModel GetById(object id)
        {
            if (IsExists(id))
            {
                SysLog entity = m_Rep.GetById(id);
                SysLogModel model = new SysLogModel();
                              				model.Id = entity.Id;
				model.Operator = entity.Operator;
				model.Message = entity.Message;
				model.Result = entity.Result;
				model.Type = entity.Type;
				model.Module = entity.Module;
				model.CreateTime = entity.CreateTime;
 
                return model;
            }
            else
            {
                return null;
            }
        }


		 /// <summary>
        /// 校验Excel数据,这个方法一般用于重写校验逻辑
        /// </summary>
        public virtual bool CheckImportData(string fileName, List<SysLogModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<SysLogModel>(x => x.Operator, "Operator");
				 excelFile.AddMapping<SysLogModel>(x => x.Message, "Message");
				 excelFile.AddMapping<SysLogModel>(x => x.Result, "Result");
				 excelFile.AddMapping<SysLogModel>(x => x.Type, "Type");
				 excelFile.AddMapping<SysLogModel>(x => x.Module, "Module");
				 excelFile.AddMapping<SysLogModel>(x => x.CreateTime, "CreateTime");
 
            //SheetName
            var excelContent = excelFile.Worksheet<SysLogModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new SysLogModel();
						 				  entity.Id = row.Id;
				  entity.Operator = row.Operator;
				  entity.Message = row.Message;
				  entity.Result = row.Result;
				  entity.Type = row.Type;
				  entity.Module = row.Module;
				  entity.CreateTime = row.CreateTime;
 
                //=============================================================================
                if (errorMessage.Length > 0)
                {
                    errors.Add(string.Format(
                        "第 {0} 列发现错误：{1}{2}",
                        rowIndex,
                        errorMessage,
                        "<br/>"));
                }
                list.Add(entity);
                rowIndex += 1;
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void SaveImportData(IEnumerable<SysLogModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        SysLog entity = new SysLog();
                       						entity.Id = ResultHelper.NewId;
						entity.Operator = model.Operator;
						entity.Message = model.Message;
						entity.Result = model.Result;
						entity.Type = model.Type;
						entity.Module = model.Module;
						entity.CreateTime = ResultHelper.NowTime;
 
                        db.SysLog.Add(entity);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
		public virtual bool Check(ref ValidationErrors errors, object id,int flag)
        {
			return true;
		}

        public virtual bool IsExists(object id)
        {
            return m_Rep.IsExist(id);
        }
		
		public void Dispose()
        { 
            
        }

	}
}
