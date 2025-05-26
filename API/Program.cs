using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.ICommand;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Infraestructure.Repository;
using Proyecto_Software_Individual.Infraestructure.Repository.Command;
using Proyecto_Software_Individual.Infraestructure.Repository.Query;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Infraestructure.Data;
using Proyecto_Software_Individual.Infraestructure.UnitOfWork;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Proyecto_Software_Individual.Aplication.Service.ServiceApprovalStatus;
using Proyecto_Software_Individual.Aplication.Service.ServiceApproverRole;
using Proyecto_Software_Individual.Aplication.Service.ServiceArea;
using Proyecto_Software_Individual.Aplication.Service.ServiceProjectApprovalStep;
using Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal;
using Proyecto_Software_Individual.Aplication.Service.ServiceProjectType;
using Proyecto_Software_Individual.Aplication.Service.ServiceUser;
using Proyecto_Software_Individual.Aplication.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AplicationDbContext>(option =>                                                          
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});


builder.Services.AddAutoMapper(typeof(AutomapperConfig));
builder.Services.AddScoped<IRepositoryApprovalRuleQuery, RepositoryApprovalRuleQuery>();
builder.Services.AddScoped<IRepositoryApprovalStatusQuery, RepositoryApprovalStatusQuery>();
builder.Services.AddScoped<IRepositoryApproverRoleQuery, RepositoryApproverRoleQuery>();
builder.Services.AddScoped<IRepositoryAreaQuery, RepositoryAreaQuery>();
builder.Services.AddScoped<IRepositoryProjectApprovalStepCommand, RepositoryProjectApprovalStepCommand>();
builder.Services.AddScoped<IRepositoryProjectApprovalStepQuery, RepositoryProjectApprovalStepQuery>();
builder.Services.AddScoped<IRepositoryProjectProposalCommand, RepositoryProjectProposalCommand>();
builder.Services.AddScoped<IRepositoryProjectProposalQuery, RepositoryProjectProposalQuery>();
builder.Services.AddScoped<IRepositoryProjectTypeQuery, RepositoryProjectTypeQuery>();
builder.Services.AddScoped<IRepositoryUserQuery, RepositoryUserQuery>();
builder.Services.AddScoped<IServiceAreaGetAll, ServiceAreaGetAll>();
builder.Services.AddScoped<IServiceProjectApprovalStepUpdate, ServiceProjectApprovalStepUpdate>();
builder.Services.AddScoped<IServiceProjectProposalCreate, ServiceProjectProposalCreate>();
builder.Services.AddScoped<IServiceProjectProposalGetById, ServiceProjectProposalGetById>();
builder.Services.AddScoped<IServiceProjectProposalGetFiltered, ServiceProjectProposalGetFiltered>();
builder.Services.AddScoped<IServiceProjectProposalUpdate, ServiceProjectProposalUpdate>();
builder.Services.AddScoped<IServiceProjectTypeGetAll, ServiceProjectTypeGetAll>();
builder.Services.AddScoped<IServiceUserGetAll, ServiceUserGetAll>();
builder.Services.AddScoped<IserviceApproverRoleGetAll, ServiceApproverRoleGetAll>();
builder.Services.AddScoped<IServiceApprovalStatusGetAll, ServiceApprovalStatusGetAll>();
builder.Services.AddScoped<StepValidator>();
builder.Services.AddScoped<StepUpdater>();
builder.Services.AddScoped<ProjectCreator>();
builder.Services.AddScoped<MapProjectWithStep>();
builder.Services.AddScoped<ProjectUpdater>();
builder.Services.AddScoped<ProjectFilters>();
builder.Services.AddScoped<IUnitOfWorkUsers, UnitOfWorkUsers>();
builder.Services.AddScoped<IUnitOfWorkCatalogs, UnitOfWorkCatalogs>();
builder.Services.AddScoped<IUnitOfWorkProjects, UnitOfWorkProjects>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sistema de Aprobación de Proyectos",
        Version = "v1",
        Description = "API desarrollada para la gestión y aprobación de propuestas de proyecto."
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
