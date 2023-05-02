using Common.Models;
using Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappers
{
    [Mapper]
    public partial class MediaMapper
    {
        public partial MediaDto Map(Media media);
        public partial Media ReverseMap(MediaDto mediaDto);
    }
}
