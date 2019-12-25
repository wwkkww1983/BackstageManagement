﻿using BackstageManagement.Model.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement
{
    public class JWTHelper
    {
        //私钥
        private static string secret = System.Configuration.ConfigurationManager.AppSettings["jwtSecret"] + "";

        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="payload">不敏感的用户数据</param>
        /// <returns></returns>
        public static string SetJwtEncode(SystemUserEntity payload)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }

        /// <summary>
        /// 根据jwtToken获取实体
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static SystemUserEntity GetJwtDecode(string token)
        {
            if (String.IsNullOrEmpty(token)) return null;
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer,provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            return decoder.DecodeToObject<SystemUserEntity>(token, secret, true);
        }

    }
}