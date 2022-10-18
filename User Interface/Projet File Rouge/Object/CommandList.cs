using Newtonsoft.Json;
using System;

namespace Projet_File_Rouge.Object
{
    public class CommandList : BDDObject
    {
        [JsonIgnore]
        [JsonProperty]
        private readonly int id;
        [JsonProperty]
        private DateTime deliveryDate;
        [JsonProperty]
        private string name;
        [JsonProperty]
        private readonly RedWire redWire;
        [JsonProperty]
        private CommandStatusEnum state;
        [JsonProperty]
        private string url;

        public CommandList(RedWire _redWire, DateTime _deliveryDate, string _name, string _url)
        {
            state = CommandStatusEnum.commande_en_attente;
            deliveryDate = new DateTime();
            redWire = _redWire;
            name = _name;
            url = _url;
        }

        [JsonConstructor]
        public CommandList(int id, CommandStatusEnum state, RedWire redWire, DateTime deliveryDate, string name, string url)
            : this (redWire, deliveryDate, name, url)
        {
            this.id = id;
            this.state = state;
            this.deliveryDate = deliveryDate;
        }

        public enum CommandStatusEnum
        {
            commande_en_attente,
            livraison_en_cours,
            livré,
            annulé
        }

        public int Id { get => id; }
        public RedWire RedWire { get => redWire; }
        public CommandStatusEnum State { get => state; set => state = value; }
        public DateTime DeliveryDate { get => deliveryDate; set => deliveryDate = value; }
        public string DeliveryDateFormated
        {
            get
            {
                if (DeliveryDate != new DateTime())
                {
                    return DeliveryDate.ToString("dd'-'MM'-'yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Name { get => name; set => name = value; }
        public string Url { get => url; set => url = value; }

        public string Jsonify()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
