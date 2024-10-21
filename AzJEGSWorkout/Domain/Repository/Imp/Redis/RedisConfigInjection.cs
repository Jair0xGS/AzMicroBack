using Domain.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Repository.Imp.Redis;

public static class RedisConfigInjection
{
        public static IServiceCollection AddRedisRepositoryImp(
            this IServiceCollection services
        )
        {
            services.AddSingleton<RedisConnection>();
            services.AddScoped<IExerciseRepo,ExerciseRepo>();
            return services;
        }
    }
