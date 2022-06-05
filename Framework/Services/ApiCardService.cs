using System.Collections.Generic;
using System.Linq;
using Framework.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Framework.Services
{
    public class ApiCardService : ICardService
    {

        public const string CARDS_API = "https://statsroyale.com/api/cards";
        public async Task<IList<Card>> GetAllCardsAsync()
        {

            var client = new RestClient(CARDS_API);
            var request = new RestRequest()
            {
                Method = Method.Get,
                RequestFormat = DataFormat.Json
            };
            

            var response = await client.ExecuteAsync(request);
            
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new System.Exception("/cards endpoint failed with " + response.StatusCode);
            }

            return JsonConvert.DeserializeObject<IList<Card>>(response.Content);
            
        }
        // public Card GetCardByName(string cardName)
        // {
        //     var cards = GetAllCardsAsync();
        //     for(int i = 0; i<4; i++){
                
        //         if (cards.Result[i].Name == cardName)
        //         {
        //             return cards;
        //         }
        //     }
        //     var t = new IceSpiritCard();
        //     return t;
   
        // }
         public Card GetCardByName(string cardName)
        {
            var cards = GetAllCardsAsync().Result;
            return cards.FirstOrDefault(card => card.Name == cardName);
        }
    }
}