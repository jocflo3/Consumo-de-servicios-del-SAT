using System.Text;

namespace Descargar_CFDIS.SAT.Clients
{
    public class SatSoapClient
    {
        private readonly HttpClient _httpClient;

        public SatSoapClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostAsync(string xml,string endpoint,string soapAction, string token ="")
        {
            StringContent content = new StringContent(xml,Encoding.UTF8,"text/xml");

            content.Headers.ContentType!.CharSet = "utf-8";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,endpoint);

            request.Headers.Add("SOAPAction",soapAction);
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Add("Authorization", $"WRAP access_token=\"{token}\"");
            }

            request.Content = content;

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            string responseXml = await response.Content.ReadAsStringAsync();

            return responseXml;
        }
    }
}