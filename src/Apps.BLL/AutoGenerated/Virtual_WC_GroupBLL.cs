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
using Apps.IDAL.WC;
using Apps.Models.WC;
using Apps.IBLL.WC;
namespace Apps.BLL.WC
{
	public partial class WC_GroupBLL: Virtual_WC_GroupBLL,IWC_GroupBLL
	{
        

	}
	public class Virtual_WC_GroupBLL
	{
        [Dependency]
        public IWC_GroupRepository m_Rep { get; set; }

		public virtual List<WC_GroupModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<WC_Group> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								
								|| a.OfficalAccountId.Contains(queryStr)
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

		public virtual List<WC_GroupModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<WC_GroupModel>();
		}
		
		public virtual List<WC_GroupModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<WC_GroupModel>();
		}

        public virtual List<WC_GroupModel> CreateModelList(ref IQueryable<WC_Group> queryData)
        {

            List<WC_GroupModel> modelList = (from r in queryData
                                              select new WC_GroupModel
                                              {
													Id = r.Id,
													Name = r.Name,
													Count = r.Count,
													OfficalAccountId = r.OfficalAccountId,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, WC_GroupModel model)
        {
            try
            {
                WC_Group entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new WC_Group();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.Count = model.Count;
				entity.OfficalAccountId = model.OfficalAccountId;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, WC_GroupModel model)
        {
            try
            {
                WC_Group entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.Count = model.Count;
				entity.OfficalAccountId = model.OfficalAccountId;
 


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

      

        public virtual WC_GroupModel GetById(object id)
        {
            if (IsExists(id))
            {
                WC_Group entity = m_Rep.GetById(id);
                WC_GroupModel model = new WC_GroupModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.Count = entity.Count;
				model.OfficalAccountId = entity.OfficalAccountId;
 
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
        public virtual bool CheckImportData(string fileName, List<WC_GroupModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<WC_GroupModel>(x => x.Name, "Name");
				 excelFile.AddMapping<WC_GroupModel>(x => x.Count, "Count");
				 excelFile.AddMapping<WC_GroupModel>(x => x.OfficalAccountId, "OfficalAccountId");
 
            //SheetName
            var excelContent = excelFile.Worksheet<WC_GroupModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new WC_GroupModel();
						 				  entity.Id = row.Id;
				  entity.Name = row.Name;
				  entity.Count = row.Count;
				  entity.OfficalAccountId = row.OfficalAccountId;
 
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
        public virtual void SaveImportData(IEnumerable<WC_GroupModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        WC_Group entity = new WC_Group();
                       						entity.Id = ResultHelper.NewId;
						entity.Name = model.Name;
						entity.Count = model.Count;
						entity.OfficalAccountId = model.OfficalAccountId;
 
                        db.WC_Group.Add(entity);
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
