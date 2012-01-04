﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.TypeMapping
{
    using Configuration;
    using Util;

    public class DefaultTypeMapper : ITypeMapper
    {
        private static Logger logger = InternalTrace.GetLogger(typeof(DefaultTypeMapper));

        public LanguageType GetLanguageType(string database, string language, string dbDataTypeName)
        {
            database = database.ToLower();
            language = language.ToLower();
            dbDataTypeName = dbDataTypeName.ToLower();
            string name = String.Format("{0}-{1}", database.ToLower(), language.ToLower());
            TypeMappingElementCollection mappings = ConfigManager.TypeMappingSection.Mappings;

            if (mappings[name] == null)
            {
                logger.Info(string.Format("Not Found {0} To {1} Data Type Mapping", database, language));
                return null;
                //throw new ArgumentNullException(name, string.Format("Not Found {0} To {1} Data Type Mapping", database, language));
            }

            TypeElement dbType = null;
            if (mappings[name].Types[dbDataTypeName] == null)
            {
                dbType = mappings[name].Types["default"];
                logger.Info(string.Format("Not Found {0} {1} Data Type Item", database, dbDataTypeName));
                //throw new ArgumentNullException(name, string.Format("Not Found {0} {1} Data Type Item", database, dbDataTypeName));
            }
            else
            {
                dbType = mappings[name].Types[dbDataTypeName];
            }

            return new LanguageType(dbType.LanguageType, dbType.DefaultValue);
        }
    }
}
