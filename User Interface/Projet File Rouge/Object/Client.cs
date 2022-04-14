
namespace Projet_File_Rouge.Object
{
    class Client : BDDObject
    {
        private readonly int? id;

        private readonly string name;

        private readonly string email;
        private readonly string phoneNumber;
        private readonly string address;

        public Client(string _name, string _email, string _phoneNumber, string _address) : this(null, _name, _email, _phoneNumber, _address) { }

        public Client(int? _id, string _name, string _email, string _phoneNumber, string _address)
        {
            id = _id;
            name = _name;
            email = _email;
            phoneNumber = _phoneNumber;
            address = _address;
        }

        public int Id { get => id != null ? (int)id : -1; }
        public string Name { get => name; }
        public string Email { get => email; }
        public string PhoneNumber { get => phoneNumber; }
        public string Address { get => address; }

        public string JsonifyId()
        {
            return "{" +
                   "\"" + ClientEnum.id + "\" : " + Id + "," +
                   "\"" + ClientEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + ClientEnum.email + "\" : \"" + Email + "\"," +
                   "\"" + ClientEnum.phoneNumber + "\" : \"" + PhoneNumber + "\"," +
                   "\"" + ClientEnum.address + "\" : \"" + Address + "\"" +
                   "}";
        }

        public string Jsonify()
        {
            return "{" +
                   "\"" + ClientEnum.name + "\" : \"" + Name + "\"," +
                   "\"" + ClientEnum.email + "\" : \"" + Email + "\"," +
                   "\"" + ClientEnum.phoneNumber + "\" : \"" + PhoneNumber + "\"," +
                   "\"" + ClientEnum.address + "\" : \"" + Address + "\"" +
                   "}";
        }
    }

    public enum ClientEnum
    {
        id,
        name,
        email,
        phoneNumber,
        address
    }
}
