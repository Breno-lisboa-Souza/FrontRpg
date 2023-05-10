using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RpgMvc.Models;



namespace RpgMvc.Controllers
{
    
    public class UsuariosController : Controller
    {
        public string uriBase = "http://localhost:5129/Usuarios/";

        [HttpGet]
        public IActionResult Index()
        {
            return View("CadastrarUsuario");
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAsync(UsuarioViewModel u)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string uriComplementar = "Registrar";

                var content = new StringContent(JsonConvert.SerializeObject(u));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/Json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Usuário {0} Registrado com sucesso! Faça o login para acessar.", u.Username);
                    return View("AutenticarUsuario");
                }
                else
                {
                    throw new System.Exception(serialized);
                }
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        

        










































































    }
}