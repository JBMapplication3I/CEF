// <copyright file="SchemaParser.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the schema parser class</summary>
#if FALSE
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Schema;

    public class SchemaParser
    {
        public void Import()
        {
            var schema = new SchemaReference
            {
                BaseUri = new Uri("http://json-schema.org/draft-06/schema#"),
            };
            var jschema = new JSchema
            {
            };
            var scope = new Scope();
            var validator = new Newtonsoft.Json.Schema.JsonValidatorContext(null, jschema);
            var schemaJson = @"{
  'description': 'A person',
  'type': 'object',
  'properties':
  {
    'name': {'type':'string'},
    'hobbies': {
      'type': 'array',
      'items': {'type':'string'}
    }
  }
}";
            var schema2 = JSchema.Parse(schemaJson, new JSchemaUrlResolver().GetSchemaResource(new ResolveSchemaContext(), schema));
var person = JObject.Parse(@"{
  'name': 'James',
  'hobbies': ['.NET', 'Blogging', 'Reading', 'Xbox', 'LOLCATS']
}");
            var valid = person.IsValid(schema);
            // true
        }
    }
}
#endif
