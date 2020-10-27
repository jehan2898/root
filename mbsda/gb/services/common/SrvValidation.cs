using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using gb.mbs.da.service.util;
using gb.mbs.da.dbconstant;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.common
{
    public class SrvValidation
    {
        public bool UserValidation(gbmodel.user.User oUser)
        {
            if (oUser == null)
                throw new  da.common.exception.IncompleteDataException("Bad Request. User object is null");
            if (oUser.Account == null)
                throw new da.common.exception.IncompleteDataException("Bad Request. User.Account object is null");
            if (oUser.Account.ID == null || oUser.Account.ID=="")
                throw new da.common.exception.IncompleteDataException("Bad Request. User.Account.ID object is null");
            if (oUser.ID == null || oUser.ID =="")
                throw new da.common.exception.IncompleteDataException("Bad Request. User.ID object is null");
            if (oUser.Token == null || oUser.ID == "")
                throw new da.common.exception.IncompleteDataException("Bad Request. User.Token object is null");
            if (oUser.Domain == null || oUser.ID == "")
                throw new da.common.exception.IncompleteDataException("Bad Request. User.Domain object is null");
            if (oUser.UserName == null || oUser.ID == "")
                throw new da.common.exception.IncompleteDataException("Bad Request. User.UserName object is null");
            return true;
        }
    }
}