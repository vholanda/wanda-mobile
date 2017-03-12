using System;
using System.Collections.Generic;
using System.Text;

namespace WandaWHTW
{
    public class LoginResult
    {
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }
		public Guid conversationId { get; set; }
	}
}
