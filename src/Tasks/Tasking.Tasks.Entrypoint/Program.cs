using Tasking.Tasks.Entrypoint;

const string ProjectTypeEnvVarName = "PROJECT_TYPE";
const string ApiProjectType = "API";

var projectType = Environment.GetEnvironmentVariable(ProjectTypeEnvVarName);

if (string.IsNullOrWhiteSpace(projectType))
    throw new ArgumentNullException(nameof(projectType));

var app = projectType switch
{
    ApiProjectType => HostFactory.CreateApi(args),
    _ => throw new NotImplementedException()
};

app.Run();