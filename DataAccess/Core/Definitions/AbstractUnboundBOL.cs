using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public abstract class AbstractUnboundBOL : IBOL
    {
        private static int sequenceManager = 0;

        #region Constructors
        public AbstractUnboundBOL()
        {
            State = ObjectState.ADDED;
            UntypedRecord = null;
        }
        public AbstractUnboundBOL(IRecord untypedRecord)
        {
            State = ObjectState.ADDED;
            UntypedRecord = untypedRecord;
        }
        #endregion

        #region Properties
        public ObjectState State { get; set; }

        public Dictionary<Type, object> SubBols { get; private set; }

        public IRecord UntypedRecord { get; private set; }
        #endregion

        #region Methods
        protected int GetNewSequence()
        {
            return ++sequenceManager;
        }
        #endregion
    }
}
