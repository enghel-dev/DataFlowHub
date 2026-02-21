using DataFlowHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataFlowHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registro de Servicios de Negocio
            services.AddScoped<ClassroomService>();
            services.AddScoped<CourseService>();
            services.AddScoped<EnrollmentService>();
            services.AddScoped<FinancialTransactionService>();
            services.AddScoped<GradeService>();
            services.AddScoped<MajorService>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<SchoolTermService>();
            services.AddScoped<StudentService>();
            services.AddScoped<TeacherService>();
            services.AddScoped<UserService>();

            return services;
        }
    }
}