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
using Apps.IDAL.Flow;
using Apps.Models.Flow;
using Apps.IBLL.Flow;
namespace Apps.BLL.Flow
{
	public partial class Flow_FormContentStepCheckBLL: Virtual_Flow_FormContentStepCheckBLL,IFlow_FormContentStepCheckBLL
	{
        

	}
	public class Virtual_Flow_FormContentStepCheckBLL
	{
        [Dependency]
        public IFlow_FormContentStepCheckRepository m_Rep { get; set; }

		public virtual List<Flow_FormContentStepCheckModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_FormContentStepCheck> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.ContentId.Contains(queryStr)
								|| a.StepId.Contains(queryStr)
								
								
								
								
								
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

		public virtual List<Flow_FormContentStepCheckModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<Flow_FormContentStepCheckModel>();
		}
		
		public virtual List<Flow_FormContentStepCheckModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<Flow_FormContentStepCheckModel>();
		}

        public virtual List<Flow_FormContentStepCheckModel> CreateModelList(ref IQueryable<Flow_FormContentStepCheck> queryData)
        {

            List<Flow_FormContentStepCheckModel> modelList = (from r in queryData
                                              select new Flow_FormContentStepCheckModel
                                              {
													Id = r.Id,
													ContentId = r.ContentId,
													StepId = r.StepId,
													State = r.State,
													StateFlag = r.StateFlag,
													CreateTime = r.CreateTime,
													IsEnd = r.IsEnd,
													IsCustom = r.IsCustom,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, Flow_FormContentStepCheckModel model)
        {
            try
            {
                Flow_FormContentStepCheck entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_FormContentStepCheck();
               				entity.Id = model.Id;
				entity.ContentId = model.ContentId;
				entity.StepId = model.StepId;
				entity.State = model.State;
				entity.StateFlag = model.StateFlag;
				entity.CreateTime = model.CreateTime;
				entity.IsEnd = model.IsEnd;
				entity.IsCustom = model.IsCustom;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, Flow_FormContentStepCheckModel model)
        {
            try
            {
                Flow_FormContentStepCheck entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.ContentId = model.ContentId;
				entity.StepId = model.StepId;
				entity.State = model.State;
				entity.StateFlag = model.StateFlag;
				entity.CreateTime = model.CreateTime;
				entity.IsEnd = model.IsEnd;
				entity.IsCustom = model.IsCustom;
 


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

      

        public virtual Flow_FormContentStepCheckModel GetById(object id)
        {
            if (IsExists(id))
            {
                Flow_FormContentStepCheck entity = m_Rep.GetById(id);
                Flow_FormContentStepCheckModel model = new Flow_FormContentStepCheckModel();
                              				model.Id = entity.Id;
				model.ContentId = entity.ContentId;
				model.StepId = entity.StepId;
				model.State = entity.State;
				model.StateFlag = entity.StateFlag;
				model.CreateTime = entity.CreateTime;
				model.IsEnd = entity.IsEnd;
				model.IsCustom = entity.IsCustom;
 
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
        public virtual bool CheckImportData(string fileName, List<Flow_FormContentStepCheckModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.ContentId, "ContentId");
				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.StepId, "StepId");
				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.State, "State");
				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.StateFlag, "StateFlag");
				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.IsEnd, "IsEnd");
				 excelFile.AddMapping<Flow_FormContentStepCheckModel>(x => x.IsCustom, "IsCustom");
 
            //SheetName
            var excelContent = excelFile.Worksheet<Flow_FormContentStepCheckModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new Flow_FormContentStepCheckModel();
						 				  entity.Id = row.Id;
				  entity.ContentId = row.ContentId;
				  entity.StepId = row.StepId;
				  entity.State = row.State;
				  entity.StateFlag = row.StateFlag;
				  entity.CreateTime = row.CreateTime;
				  entity.IsEnd = row.IsEnd;
				  entity.IsCustom = row.IsCustom;
 
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
        public virtual void SaveImportData(IEnumerable<Flow_FormContentStepCheckModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        Flow_FormContentStepCheck entity = new Flow_FormContentStepCheck();
                       						entity.Id = ResultHelper.NewId;
						entity.ContentId = model.ContentId;
						entity.StepId = model.StepId;
						entity.State = model.State;
						entity.StateFlag = model.StateFlag;
						entity.CreateTime = ResultHelper.NowTime;
						entity.IsEnd = model.IsEnd;
						entity.IsCustom = model.IsCustom;
 
                        db.Flow_FormContentStepCheck.Add(entity);
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
