<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuthBlock.ascx.cs" Inherits="Clarity.Ecommerce.DNN.Extensions.CEFComponents.AuthBlock" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="DotNetNuke.Entities.Users" %>
<% if (Request.IsAuthenticated) { %>
<asp:HiddenField ID="hdnCurrentUsername" runat="server" />
<asp:HiddenField ID="hdnCurrentToken" runat="server" />
<script type="C#" runat="server">
    private const string PasswordHash = "qpzm9731";
    private const string SaltKey = "Cl4r1tyV3ntur3s";
    private const string VIKey = "46g7HeDF@52c831B";

    private static string Encrypt(string plainText, bool isUrlEncoded = false)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        byte[] cipherTextBytes;
        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
            }
        }
        var output = Convert.ToBase64String(cipherTextBytes);
        if (isUrlEncoded) { output = HttpUtility.UrlEncode(output); }
        return output;
    }

    private static IEnumerable<byte> GetBytes(string str)
    {
        var bytes = new byte[str.Length * sizeof(char)];
        Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    private readonly string token = UserController.Instance == null
            || UserController.Instance.GetCurrentUserInfo() == null
            || UserController.Instance.GetCurrentUserInfo().Username == null
        ? string.Empty
        : Encrypt(
            Convert.ToBase64String(
                BitConverter.GetBytes(DateTime.UtcNow.ToBinary())
                    .Concat(GetBytes("|"))
                    .Concat(Guid.NewGuid().ToByteArray())
                    .Concat(GetBytes("|"))
                    .Concat(GetBytes(UserController.Instance.GetCurrentUserInfo().Username))
                    .ToArray()
                )
            );
</script>
<script>
$("#<%=hdnCurrentUsername.ClientID %>").attr("value","<%=UserController.Instance.GetCurrentUserInfo().Username%>");
$("#<%=hdnCurrentToken.ClientID %>").attr("value","<%=token%>");
</script>
<% } %>