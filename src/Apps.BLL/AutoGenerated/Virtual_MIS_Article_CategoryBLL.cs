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
using Apps.IDAL.MIS;
using Apps.Models.MIS;
using Apps.IBLL.MIS;
namespace Apps.BLL.MIS
{
	public partial class MIS_Article_CategoryBLL: Virtual_MIS_Article_CategoryBLL,IMIS_Article_CategoryBLL
	{
        

	}
	public class Virtual_MIS_Article_CategoryBLL
	{
        [Dependency]
        public IMIS_Article_CategoryRepository m_Rep { get; set; }

		public virtual List<MIS_Article_CategoryModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<MIS_Article_Category> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								
								|| a.Name.Contains(queryStr)
								|| a.ParentId.Contains(queryStr)
								
								|| a.ImgUrl.Contains(queryStr)
								|| a.BodyContent.Contains(queryStr)
								
								
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

		public virtual List<MIS_Article_CategoryModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<MIS_Article_CategoryModel>();
		}
		
		public virtual List<MIS_Article_CategoryModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<MIS_Article_CategoryModel>();
		}

        public virtual List<MIS_Article_CategoryModel> CreateModelList(ref IQueryable<MIS_Article_Category> queryData)
        {

            List<MIS_Article_CategoryModel> modelList = (from r in queryData
                                              select new MIS_Article_CategoryModel
                                              {
													Id = r.Id,
													ChannelId = r.ChannelId,
													Name = r.Name,
													ParentId = r.ParentId,
													Sort = r.Sort,
													ImgUrl = r.ImgUrl,
													BodyContent = r.BodyContent,
													CreateTime = r.CreateTime,
													Enable = r.Enable,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, MIS_Article_CategoryModel model)
        {
            try
            {
                MIS_Article_Category entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new MIS_Article_Category();
               				entity.Id = model.Id;
				entity.ChannelId = model.ChannelId;
				entity.Name = model.Name;
				entity.ParentId = model.ParentId;
				entity.Sort = model.Sort;
				entity.ImgUrl = model.ImgUrl;
				entity.BodyContent = model.BodyContent;
				entity.CreateTime = model.CreateTime;
				entity.Enable = model.Enable;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, MIS_Article_CategoryModel model)
        {
            try
            {
                MIS_Article_Category entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.ChannelId = model.ChannelId;
				entity.Name = model.Name;
				entity.ParentId = model.ParentId;
				entity.Sort = model.Sort;
				entity.ImgUrl = model.ImgUrl;
				entity.BodyContent = model.BodyContent;
				entity.CreateTime = model.CreateTime;
				entity.Enable = model.Enable;
 


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

      

        public virtual MIS_Article_CategoryModel GetById(object id)
        {
            if (IsExists(id))
            {
                MIS_Article_Category entity = m_Rep.GetById(id);
                MIS_Article_CategoryModel model = new MIS_Article_CategoryModel();
                              				model.Id = entity.Id;
				model.ChannelId = entity.ChannelId;
				model.Name = entity.Name;
				model.ParentId = entity.ParentId;
				model.Sort = entity.Sort;
				model.ImgUrl = entity.ImgUrl;
				model.BodyContent = entity.BodyContent;
				model.CreateTime = entity.CreateTime;
				model.Enable = entity.Enable;
 
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
        public virtual bool CheckImportData(string fileName, List<MIS_Article_CategoryModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.ChannelId, "ChannelId");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.Name, "Name");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.ParentId, "ParentId");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.Sort, "Sort");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.ImgUrl, "ImgUrl");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.BodyContent, "BodyContent");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<MIS_Article_CategoryModel>(x => x.Enable, "Enable");
 
            //SheetName
            var excelContent = excelFile.Worksheet<MIS_Article_CategoryModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new MIS_Article_CategoryModel();
						 				  entity.Id = row.Id;
				  entity.ChannelId = row.ChannelId;
				  entity.Name = row.Name;
				  entity.ParentId = row.ParentId;
				  entity.Sort = row.Sort;
				  entity.ImgUrl = row.ImgUrl;
				  entity.BodyContent = row.BodyContent;
				  entity.CreateTime = row.CreateTime;
				  entity.Enable = row.Enable;
 
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
        public virtual void SaveImportData(IEnumerable<MIS_Article_CategoryModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        MIS_Article_Category entity = new MIS_Article_Category();
                       						entity.Id = ResultHelper.NewId;
						entity.ChannelId = model.ChannelId;
						entity.Name = model.Name;
						entity.ParentId = model.ParentId;
						entity.Sort = model.Sort;
						entity.ImgUrl = model.ImgUrl;
						entity.BodyContent = model.BodyContent;
						entity.CreateTime = ResultHelper.NowTime;
						entity.Enable = model.Enable;
 
                        db.MIS_Article_Category.Add(entity);
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
