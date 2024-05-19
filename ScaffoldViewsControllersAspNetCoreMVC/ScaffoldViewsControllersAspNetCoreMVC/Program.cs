
using System;
using System.Collections.Generic;
using System.IO;

string nomeEntidadeNoControlador = "Filme2rr";
string nomeEntidadeNoModelo = "Filmer";
string nomeEntidadeNoModeloPlural = "Filmesrr";

string localDoProjeto = @"C:\Users\leomi\Desktop\.net\Filmes_Mvc\Filmes_Mv\";
string localDaPastaDasVisoesNoProjeto = localDoProjeto + @"Views\" + nomeEntidadeNoControlador;
string localDaPastaDosControladoresNoProjeto = localDoProjeto + @"Controllers\";

Dictionary<string, List<(string, string)>> substituicoesView = new Dictionary<string, List<(string, string)>>
        {
            { "Create.cshtml", new List<(string, string)> {
                ("\"Create\";", "\"Cadastro\";"),
                (">Create</h1>", ">Cadastro</h1><br />"),
                ("<input type=\"submit\" value=\"Create\" class=\"btn btn-primary\" />", "<button type=\"submit\" class=\"btn btn-success\">\r\n                    <i class=\"bi bi-file-earmark-plus\"></i> Cadastrar\r\n                </button>"),
                (">Back to List<", ">Voltar para a Listagem<"),
                ("class=\"form-group\">", "class=\"form-group mb-3\">"),
                ("asp-action=\"Index\">", "asp-action=\"Index\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-box-arrow-down-left\"></i> "),
                ("class=\"control-label\">", "class=\"control-label fw-bold\">"),
                ("class=\"btn btn-primary\"", "class=\"btn btn-success\"")
            }},
            { "Delete.cshtml", new List<(string, string)> {
                ("\"Delete\";", "\"Exclusão\";"),
                (">Delete</h1>", ">Exclusão</h1><br />"),
                ("3>Are you sure you want to delete this?</h3>", "5>Excluir permanentemente este registro?</h5><br />"),
                ("<input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" />", "<button type=\"submit\" class=\"btn btn-success\">\r\n            <i class=\"bi bi-trash3-fill\"></i> Excluir\r\n        </button>"),
                (">Back to List<", ">Voltar para a Listagem<"),
                ("asp-action=\"Index\">", "asp-action=\"Index\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-box-arrow-down-left\"></i> ")
            }},
            { "Details.cshtml", new List<(string, string)> {
                ("\"Details\";", "\"Detalhes\";"),
                (">Details</h1>", ">Detalhes</h1><br />"),
                (">Edit</a>", ">Alterar</a>"),
                (">Back to List<", ">Voltar para a Listagem<"),
                ("asp-action=\"Index\">", "asp-action=\"Index\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-box-arrow-down-left\"></i> "),
                ("asp-route-id=\"@Model?.Id\">", "asp-route-id=\"@Model?.Id\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-pencil-square\"></i> ")
            }},
            { "Edit.cshtml", new List<(string, string)> {
                ("\"Edit\";", "\"Alteração\";"),
                (">Edit</h1>", ">Alteração</h1><br />"),
                ("<input type=\"submit\" value=\"Save\" class=\"btn btn-primary\" />", "<button type=\"submit\" class=\"btn btn-success\">\r\n                    <i class=\"bi bi-pencil-square\"></i> Alterar\r\n                </button>"),
                (">Back to List<", ">Voltar para a Listagem<"),
                ("class=\"form-group\">", "class=\"form-group mb-3\">"),
                ("asp-action=\"Index\">", "asp-action=\"Index\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-box-arrow-down-left\"></i> "),
                ("class=\"control-label\">", "class=\"control-label fw-bold\">"),
                ("class=\"btn btn-primary\"", "class=\"btn btn-success\"")

            }},
            { "Index.cshtml", new List<(string, string)> {
                ("\"Index\";", "\"Listagem de " + nomeEntidadeNoModeloPlural + "\";"),
                (">Index</h1>", ">Listagem de " + nomeEntidadeNoModeloPlural + "</h1><br />\r\n@if (TempData[\"MensagemSucesso\"] != null)\r\n{\r\n    <div class=\"alert alert-success fs-6\" role=\"alert\">\r\n        <b>\r\n            @TempData[\"MensagemSucesso\"]\r\n        </b>\r\n    </div>\r\n}"),
                ("Create\">Create New<", "Create\" class=\"btn btn-secondary\"><i class=\"bi bi-file-earmark-plus\"></i> Cadastrar<"),
                (">Edit</a>", ">Alterar</a>"),
                (">Details</a>", ">Detalhar</a>"),
                (">Delete</a>", ">Excluir</a>"),
                ("table class=\"table\"","table id=\"tabelaDeDados\" class=\"table table-striped\""),
                ("<thead>", "<thead class=\"table-light\">"),
                ("Edit\" asp-route-id=\"@item.Id\">","Edit\" asp-route-id=\"@item.Id\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-pencil-square\"></i> "),
                ("Details\" asp-route-id=\"@item.Id\">", "Details\" asp-route-id=\"@item.Id\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-file-bar-graph-fill\"></i> "),
                ("Delete\" asp-route-id=\"@item.Id\">", "Delete\" asp-route-id=\"@item.Id\" class=\"btn btn-secondary btn-sm\"><i class=\"bi bi-trash3-fill\"></i> ")
            }},
        };

Dictionary<string, List<(string, string)>> substituicoesController = new Dictionary<string, List<(string, string)>>
{
    { nomeEntidadeNoControlador + "Controller.cs", new List<(string, string)> {
                ("_context.Add(" + nomeEntidadeNoModelo.ToLower() + ");", "_context.Add(" + nomeEntidadeNoModelo.ToLower() + ");\r\n                TempData[\"MensagemSucesso\"] = \"Cadastro realizado com sucesso!\";"),
                ("_context.Update(" + nomeEntidadeNoModelo.ToLower() + ");", "_context.Update(" + nomeEntidadeNoModelo.ToLower() + ");\r\n                    TempData[\"MensagemSucesso\"] = \"Alteração realizada com sucesso!\";"),
                ("_context.Filme.Remove("  + nomeEntidadeNoModelo.ToLower() + ");", "_context.Filme.Remove("  + nomeEntidadeNoModelo.ToLower() + ");\r\n                TempData[\"MensagemSucesso\"] = \"Exclusão realizada com sucesso!\";")
            }},
};

try
{
    foreach (var par in substituicoesView)
    {
        string arquivo = par.Key;
        List<(string, string)> conjuntoSubstituicoes = par.Value;

        string caminhoArquivo = Path.Combine(localDaPastaDasVisoesNoProjeto, arquivo);

        if (File.Exists(caminhoArquivo))
        {
            string conteudo = File.ReadAllText(caminhoArquivo);

            foreach (var substituicao in conjuntoSubstituicoes)
            {
                conteudo = conteudo.Replace(substituicao.Item1, substituicao.Item2);
            }

            File.WriteAllText(caminhoArquivo, conteudo);

            Console.WriteLine($"Substituição executada em {arquivo}.");
        }
        else
        {
            Console.WriteLine($"Arquivo {arquivo} não encontrado.");
        }
    }

    foreach (var par in substituicoesController)
    {
        string arquivo = par.Key;
        List<(string, string)> conjuntoSubstituicoes = par.Value;

        string caminhoArquivo = Path.Combine(localDaPastaDosControladoresNoProjeto, arquivo);

        if (File.Exists(caminhoArquivo))
        {
            string conteudo = File.ReadAllText(caminhoArquivo);

            foreach (var substituicao in conjuntoSubstituicoes)
            {
                conteudo = conteudo.Replace(substituicao.Item1, substituicao.Item2);
            }

            File.WriteAllText(caminhoArquivo, conteudo);

            Console.WriteLine($"Substituição executada em {arquivo}.");
        }
        else
        {
            Console.WriteLine($"Arquivo {arquivo} não encontrado.");
        }
    }

    Console.WriteLine("<<< Processo de substituição finalizado com sucesso. >>>");
}
catch (Exception ex)
{
    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
}