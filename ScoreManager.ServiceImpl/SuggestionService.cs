using Models;
using ScoreManager.ServiceInterface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager.ServiceImpl
{
    public class SuggestionService : BaseService<EDU_SUGGESTIONS>, ISuggestionService
    {
        public SuggestionService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }
    }
}
