using System;

namespace Eco.Echolon.ApiClient.Model.Results
{
    public class SystemIndividuals
    {
        public bool? Is_echolon_user {get;}
        public bool? Is_selfhelp_user {get;}
        public string? Email {get;}
        public bool? Email_verified {get;}
        public string? Sub {get;}
        public string? Phone_number {get;}
        public bool? Phone_number_verified {get;}
        public DateTimeOffset? Birthdate {get;}
        public string? Family_name {get;}
        public string? Foreign_id {get;}
        public string? Gender {get;}
        public string? Given_name {get;}
        public string? Locale {get;}
        public string? Middle_name {get;}
        public string? Name {get;}
        public string? Nickname {get;}
        public string? Picture {get;}
        public string? Profile {get;}
        public DateTimeOffset? Updated_at {get;}
        public string? Website {get;}
        public string? Zoneinfo {get;}

        public SystemIndividuals(bool? isEcholonUser,
            bool? isSelfhelpUser,
            string? email,
            bool? emailVerified,
            string? sub,
            string? phoneNumber,
            bool? phoneNumberVerified,
            DateTimeOffset? birthdate,
            string? familyName,
            string? foreignId,
            string? gender,
            string? givenName,
            string? locale,
            string? middleName,
            string? name,
            string? nickname,
            string? picture,
            string? profile,
            DateTimeOffset? updatedAt,
            string? website,
            string? zoneinfo)
        {
            Is_echolon_user = isEcholonUser;
            Is_selfhelp_user = isSelfhelpUser;
            Email = email;
            Email_verified = emailVerified;
            Sub = sub;
            Phone_number = phoneNumber;
            Phone_number_verified = phoneNumberVerified;
            Birthdate = birthdate;
            Family_name = familyName;
            Foreign_id = foreignId;
            Gender = gender;
            Given_name = givenName;
            Locale = locale;
            Middle_name = middleName;
            Name = name;
            Nickname = nickname;
            Picture = picture;
            Profile = profile;
            Updated_at = updatedAt;
            Website = website;
            Zoneinfo = zoneinfo;
        }
    }
}