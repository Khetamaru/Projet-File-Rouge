using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Object
{
    public class CommandList : BDDObject
    {
        private readonly int id;
        private DateTime deliveryDate;
        private string name;
        private readonly RedWire redWire;
        private CommandStatusEnum state;
        private string url;

        public CommandList(RedWire _redWire, DateTime _deliveryDate, string _name, string _url)
        {
            id = 42;
            state = CommandStatusEnum.commande_en_attente;
            deliveryDate = new DateTime();
            redWire = _redWire;
            name = _name;
            url = _url;
        }

        public CommandList(int _id, CommandStatusEnum _state, RedWire _redWire, DateTime _deliveryDate, string _name, string _url)
            : this (_redWire, _deliveryDate, _name, _url)
        {
            id = _id;
            state = _state;
            deliveryDate = _deliveryDate;
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
        public Uri UrlUri { get => new Uri(url); }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + CommandListEnum.id + "\" : " + Id + "," +
                   "\"" + CommandListEnum.redWire + "\" : " + RedWire.Id + "," +
                   "\"" + CommandListEnum.state + "\" : " + (int)State + "," +
                   "\"" + CommandListEnum.deliveryDate + "\" : \"" + DeliveryDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + CommandListEnum.name + "\" : " + Name + "," +
                   "\"" + CommandListEnum.url + "\" : " + Url + "," +
                   "}";
        }
        public string Jsonify()
        {
            return "{" +
                   "\"" + CommandListEnum.redWire + "\" : " + RedWire.Id + "," +
                   "\"" + CommandListEnum.state + "\" : " + (int)State + "," +
                   "\"" + CommandListEnum.deliveryDate + "\" : \"" + DeliveryDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"," +
                   "\"" + CommandListEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + CommandListEnum.url + "\" : \"" + Url + "\"," +
                   "}";
        }
    }

    public enum CommandListEnum
    {
        id, 
        redWire, 
        state,
        deliveryDate, 
        name, 
        url
    }
}
