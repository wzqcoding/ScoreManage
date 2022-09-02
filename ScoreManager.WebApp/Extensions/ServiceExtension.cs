using Microsoft.Extensions.DependencyInjection;
using ScoreManager.Common;
using ScoreManager.ServiceImpl;
using ScoreManager.ServiceInterface;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtension
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IExaminPaperService, ExaminPaperService>();
            services.AddScoped<IPaperQuestionService, PaperQuestionService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamDetailService, ExamDetailService>();
            services.AddScoped<IScoreDetailService, ScoreDetailService>();
            services.AddScoped<IAppealService, AppealService>();
            services.AddScoped<ISuggestionService, SuggestionService>();
            services.AddScoped(typeof(CommonHelper));
        }
    }
}
