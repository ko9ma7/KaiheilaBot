using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace KaiheilaBot
{
    public class ExtraSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Extra).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }

    public class ExtraConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new ExtraSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Extra));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["type"].Value<object>().ToString())
            {
                case "1":
                    return JsonConvert.DeserializeObject<ExtraText>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    ((JObject)jo["body"]).Add("type", jo["type"]);
                    switch (jo["type"].Value<string>())
                    {
                        case "deleted_reaction":
                        case "added_reaction":
                            return JsonConvert.DeserializeObject<Reaction>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "updated_message":
                            return JsonConvert.DeserializeObject<UpdateMessage>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "deleted_message":
                            return JsonConvert.DeserializeObject<DeleteMessage>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "added_channel":
                            return JsonConvert.DeserializeObject<NewChannel>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "updated_channel":
                            return JsonConvert.DeserializeObject<EditChannel>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "deleted_channel":
                            return JsonConvert.DeserializeObject<DeleteChannel>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "pinned_message":
                        case "unpinned_message":
                            return JsonConvert.DeserializeObject<PinnedMessage>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "joined_guild":
                            return JsonConvert.DeserializeObject<MemberJoin>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "exited_guild":
                            return JsonConvert.DeserializeObject<MemberLeave>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "updated_guild_member":
                            return JsonConvert.DeserializeObject<MemberChangeNick>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        case "self_joined_guild":
                            return JsonConvert.DeserializeObject<Invited>(jo["body"].ToString(), SpecifiedSubclassConversion);
                        default:
                            return JsonConvert.DeserializeObject<Unknown>(jo.ToString(), SpecifiedSubclassConversion);
                    }
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}
