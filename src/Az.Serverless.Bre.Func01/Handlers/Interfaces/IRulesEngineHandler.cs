﻿using Az.Serverless.Bre.Func01.Models;
using System.Threading.Tasks;

namespace Az.Serverless.Bre.Func01.Handlers.Interfaces
{
    public interface IRulesEngineHandler
    {
        public void AddOrUpdateWorkflows(string workflowString);

        public string GetWorkflowName(string rulesCongfig);

        public Task<EvaluationOutput> ExecuteRulesAsync(string rulesConfigFile, EvaluationInput[] evaluationInputs);


    }
}
