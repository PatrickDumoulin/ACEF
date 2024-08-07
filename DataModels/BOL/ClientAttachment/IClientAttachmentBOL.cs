using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.ClientAttachment
{
    public interface IClientAttachmentBOL : IBOL
    {
        int Id { get;  }
        int? IdClient { get;  }
        int? IdEmployee { get;  }
        string FileName { get; }
        byte[] FileContent { get;  }
        DateTime? CreatedDate { get;  }
    }
}
