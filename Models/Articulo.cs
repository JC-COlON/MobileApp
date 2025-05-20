using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigesettAPP.Models
{
    public class Articulo
    {
        [JsonProperty("articleNum")]
        public string ArticleNum { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("articleId")]
        public int ArticleId { get; set; }

        // solo lectura

    }

}
