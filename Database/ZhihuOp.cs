using Repos;
using Spider.MultiDbContext;
using System.Collections.Generic;

namespace Spider.Database
{
    class ZhihuOp : IDatabaseOp
    {
        private static ZhihuOp _instance;
        public static ZhihuOp Instance
        {
            get {
                if (_instance == null) _instance = new ZhihuOp();
                return _instance;
            }
        }

        /// <summary>
        /// 保存单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void SaveData<T>(T data) where T : IRepo
        {
            using (ZhiHuContext context = new ZhiHuContext())
            {
                if (data.GetType() == typeof(HotRepo))
                    context.HotEntities.Add(data as HotRepo);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        public void SaveRangeData<T>(IEnumerable<T> datas) where T : IRepo
        {
            using (ZhiHuContext context = new ZhiHuContext())
            {
                if (typeof(T) == typeof(HotRepo))
                    context.HotEntities.AddRange(datas as IEnumerable<HotRepo>);
                context.SaveChanges();
            }
        }
    }
}
