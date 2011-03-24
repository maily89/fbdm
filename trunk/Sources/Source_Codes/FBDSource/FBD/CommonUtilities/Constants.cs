using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class Constants
    {
        /** NUMERIC VALUE */

        public const int NUMBER_OF_RANKING_STRUCTURE = 4;


        /*****************CLASS NAMES *******************/

        //CUSTOMER
        public const string CUSTOMER_BUSINESS = "Business Customer";
        public const string CUSTOMER_BUSINESS_RANKING = "Ranked Business Customer";
        public const string CUSTOMER_LOANTERM = "Loan term";

        //BUSINESS
        public const string BUSINESS_INDUSTRY = "Business Industry";
        public const string BUSINESS_LINE = "Business Line";
        public const string BUSINESS_RANK = "Business Rank";
        public const string BUSINESS_RANKING_STRUCTURE = "Business Ranking Structure";
        public const string BUSINESS_TYPE = "Business Type";
        public const string BUSINESS_SCALE = "Business Scale";
        public const string BUSINESS_SCALECRITERIA = "Business Scale Criteria";

        public const string BUSINESS_FINANCIAL_INDEX = "Business Financial Index";
        public const string BUSINESS_FINANCIAL_INDEX_LEVEL = "Business Financial Index Level";
        public const string BUSINESS_FINANCIAL_INDEX_PROPORTION = "Business Financial Index Proportion";
        public const string BUSINESS_FINANCIAL_INDEX_SCORE = "Business Financial Index Score";

        public const string BUSINESS_NON_FINANCIAL_INDEX = "Business Non-Financial Index";
        public const string BUSINESS_NON_FINANCIAL_INDEX_LEVEL = "Business Non-Financial Index Level";
        public const string BUSINESS_NON_FINANCIAL_INDEX_PROPORTION = "Business Non-Financial Index Proportion";
        public const string BUSINESS_NON_FINANCIAL_INDEX_SCORE = "Business Non-Financial Index Score";

        //SYSTEM
        public const string SYSTEM_USER_GROUP = "System User Group";
        public const string SYSTEM_RIGHT = "System Right";
        public const string SYSTEM_USER_GROUP_RIGHT = "User Groups and Rights";
        public const string SYSTEM_REPORTING_PERIOD = "System Reporting Period";
        public const string SYSTEM_USER = "System User";
        public const string SYSTEM_BRANCH = "System Branch";
        public const string SYSTEM_LIST_RIGHTS = "list of Right for this User Group";

        //INV
        public const string BORROWING_PURPOSE = "Borrowing purpose";
        public const string INV_BASIC_INDEX = "Individual Basic Index";
        public const string INV_COLLATERAL_INDEX = "Individual collateral index";
        public const string INV_BASIC_RANK = "Individual Basic rank";
        public const string INV_SUMMARY_RANK = "Individual summary rank";
        public const string INV_BASIC_INDEX_SCORE = "Individual Basic Index score";
        public const string INV_BASIC_LEVEL_INDEX = "Individual Basic level Index";
        public const string INV_COLL_LEVEL_INDEX = "Individual collateral level Index";
        public const string INV_PROPORTION = "Individual Proportion";
        public const string INV_COLL_RANK = "Individual Collateral Rank";
        


        /****************** SYSTEM RIGHTS ******************/

        public const string ACTION_PARAMETERS_VIEW = "001";
        public const string ACTION_PARAMETERS_UPDATE = "002";
        public const string ACTION_RANKING = "003";


        /***************** MESSAGE TYPES *****************/

        public const string SCC_MESSAGE = "SuccessfulMessage";
        public const string ERR_MESSAGE = "ErrorMessage";



        /************** ERROR MESSAGES ****************/

        // COMMON ERRORS
        public const string ERR_INDEX = "Error when displaying the list of {0}";
        public const string ERR_ADD_POST = "Error when adding new {0} due to duplicated ID or invalid data input. Please check again";
        public const string ERR_EDIT = "Error when select the {0}. Please try again";
        public const string ERR_EDIT_POST = "Error when editting {0}. Please recheck your input ";
        public const string ERR_DELETE = "Error when deleting the {0}. Try again later";
        public const string ERR_KEY_EXIST = "The ID already exists. Please try again!";
        public const string ERR_UNABLE_CHECK = "Error when checking ID dupplication. Please try again!";
        public const string ERR_CONTROLLER_PARSING = "Error when parsing information at server. Try again later";
        public const string ERR_INVALID_INDEX_ID = "The Index ID must be digits number!";
        
        // PROPORTION ERRORS
        public const string ERR_UPDATE_PROPORTION = "Error when updating to the index {0}. Try again later";
        public const string ERR_UPDATE_PROPORTION_CALCULATED = "Proportion changes were updated successfully but "
                                                                + "the summary proportion got errors. Please check "
                                                                + "the logic of proportion again";
        public const string ERR_UPDATE_PROPORTION_COMMON = "Error when updating propotion. Try again later";
        // SCORE ERRORS
        public const string ERR_UPDATE_SCORE = "Error when updating to the level {0}. Try again later";
        public const string ERR_UPDATE_SCORE_COMMON = "Error when updating score. Try again later";


        //SYSTEM USER GROUPS
        public const string ERR_INDEX_SYS_USER_GROUPS = "ERR_INDEX_SYS_USER_GROUPS";
        public const string ERR_ADD_POST_SYS_USER_GROUPS = "ERR_ADD_POST_SYS_USER_GROUPS";
        public const string ERR_EDIT_SYS_USER_GROUPS = "ERR_EDIT_SYS_USER_GROUPS";
        public const string ERR_EDIT_POST_SYS_USER_GROUPS = "ERR_EDIT_POST_SYS_USER_GROUPS";
        public const string ERR_DELETE_SYS_USER_GROUPS = "ERR_DELETE_SYS_USER_GROUPS";

        //SYSTEM BRANCHES
        public const string ERR_INDEX_SYS_BRANCHES = "ERR_INDEX_SYS_BRANCHES";
        public const string ERR_ADD_POST_SYS_BRANCHES = "ERR_ADD_POST_SYS_BRANCHES";
        public const string ERR_EDIT_SYS_BRANCHES = "ERR_EDIT_SYS_BRANCHES";
        public const string ERR_EDIT_POST_SYS_BRANCHES = "ERR_EDIT_POST_SYS_BRANCHES";
        public const string ERR_DELETE_SYS_BRANCHES = "ERR_DELETE_SYS_BRANCHES";
        
        public const string ERR_TO_DATE_LESS_THAN_FROM_DATE = "ERR_TO_DATE_LESS_THAN_FROM_DATE";

        //SYSTEM USERS
        public const string ERR_INDEX_SYS_USERS = "ERR_INDEX_SYS_USERS";
        public const string ERR_ADD_POST_SYS_USERS = "ERR_ADD_POST_SYS_USERS";
        public const string ERR_EDIT_SYS_USERS = "ERR_EDIT_SYS_RIGHTS";
        public const string ERR_EDIT_POST_SYS_USERS = "ERR_EDIT_POST_SYS_USERS";
        public const string ERR_DELETE_SYS_USERS = "ERR_DELETE_SYS_USERS";



        /************** SUCCESSFUL MESSAGES ****************/

        public const string SCC_ADD = "A new {0} has been added successfully";
        public const string SCC_EDIT_POST = "The {0} with ID {1} has been editted successfully";
        public const string SCC_DELETE = "A {0} has been deleted successfully";


        public const string SCC_UPDATE_PROPORTION = "Update proportion successfully";
        public const string SCC_UPDATE_SCORE = "Update score successfully";


        //SYSTEM USER GROUPS
        public const string SCC_ADD_POST_SYS_USER_GROUPS = "SCC_ADD_POST_SYS_USER_GROUPS";
        public const string SCC_EDIT_POST_SYS_USER_GROUPS_1 = "SCC_EDIT_POST_SYS_USER_GROUPS_1";
        public const string SCC_EDIT_POST_SYS_USER_GROUPS_2 = "SCC_EDIT_POST_SYS_USER_GROUPS_2";
        public const string SCC_DELETE_SYS_USER_GROUPS = "SCC_DELETE_SYS_USER_GROUPS";

        //SYSTEM BRANCHES
        public const string SCC_ADD_POST_SYS_BRANCHES = "SCC_ADD_POST_SYS_BRANCHES";
        public const string SCC_EDIT_POST_SYS_BRANCHES_1 = "SCC_EDIT_POST_SYS_BRANCHES_1";
        public const string SCC_EDIT_POST_SYS_BRANCHES_2 = "SCC_EDIT_POST_SYS_BRANCHES_2";
        public const string SCC_DELETE_SYS_BRANCHES = "SCC_DELETE_SYS_BRANCHES";

        //SYSTEM USERS
        public const string SCC_ADD_POST_SYS_USERS = "SCC_ADD_POST_SYS_USERS";
        public const string SCC_EDIT_POST_SYS_USERS_1 = "SCC_EDIT_POST_SYS_USERS_1";
        public const string SCC_EDIT_POST_SYS_USERS_2 = "SCC_EDIT_POST_SYS_USERS_2";
        public const string SCC_DELETE_SYS_USERS = "SCC_DELETE_SYS_USERS";
        

        /// <summary>
        /// Used for validating index id
        /// </summary>
        public const string INDEX_FORMAT_STRING = "0123456789";
    }
}
