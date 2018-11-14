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
	public partial class SysRightBLL: Virtual_SysRightBLL,ISysRightBLL
	{
        

	}
	public class Virtual_SysRightBLL
	{
        [Dependency]
        public ISysRightRepository m_Rep { get; set; }

		public virtual List<SysRightModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysRight> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.ModuleId.Contains(queryStr)
								|| a.RoleId.Contains(queryStr)
								
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

		public virtual List<SysRightModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<SysRightModel>();
		}
		
		public virtual List<SysRightModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<SysRightModel>();
		}

        public virtual List<SysRightModel> CreateModelList(ref IQueryable<SysRight> queryData)
        {

            List<SysRightModel> modelList = (from r in queryData
                                              select new SysRightModel
                                              {
													Id = r.Id,
													ModuleId = r.ModuleId,
													RoleId = r.RoleId,
													Rightflag = r.Rightflag,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysRightModel model)
        {
            try
            {
                SysRight entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysRight();
               				entity.Id = model.Id;
				entity.ModuleId = model.ModuleId;
				entity.RoleId = model.RoleId;
				entity.Rightflag = model.Rightflag;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, SysRightModel model)
        {
            try
            {
                SysRight entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.ModuleId = model.ModuleId;
				entity.RoleId = model.RoleId;
				entity.Rightflag = model.Rightflag;
 


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

      

        public virtual SysRightModel GetById(object id)
        {
            if (IsExists(id))
            {
                SysRight entity = m_Rep.GetById(id);
                SysRightModel model = new SysRightModel();
                              				model.Id = entity.Id;
				model.ModuleId = entity.ModuleId;
				model.RoleId = entity.RoleId;
				model.Rightflag = entity.Rightflag;
 
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
        public virtual bool CheckImportData(string fileName, List<SysRightModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<SysRightModel>(x => x.ModuleId, "ModuleId");
				 excelFile.AddMapping<SysRightModel>(x => x.RoleId, "RoleId");
				 excelFile.AddMapping<SysRightModel>(x => x.Rightflag, "Rightflag");
 
            //SheetName
            var excelContent = excelFile.Worksheet<SysRightModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new SysRightModel();
						 				  entity.Id = row.Id;
				  entity.ModuleId = row.ModuleId;
				  entity.RoleId = row.RoleId;
				  entity.Rightflag = row.Rightflag;
 
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
        public virtual void SaveImportData(IEnumerable<SysRightModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        SysRight entity = new SysRight();
                       						entity.Id = ResultHelper.NewId;
						entity.ModuleId = model.ModuleId;
						entity.RoleId = model.RoleId;
						entity.Rightflag = model.Rightflag;
 
                        db.SysRight.Add(entity);
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
