﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.AttributeHandlers
{
    interface IAttributeHandler
    {
        IAttributeHandler clone();

        void handle(Context context, XElement xml);
    }
}
