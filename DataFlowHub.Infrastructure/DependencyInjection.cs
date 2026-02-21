using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using DataFlowHub.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DataFlowHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // La conexión a DB (ADO.NET Factory)
            services.AddSingleton<DBconnectionFactory>();

            // Registro de todos los Repositorios ADO.NET
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISchoolTermRepository, SchoolTermRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}