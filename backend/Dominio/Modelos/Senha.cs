using System;
using System.Linq;
using System.Security.Cryptography;

namespace Agenda.Dominio.Modelos
{
  public class Senha
  {
    public string GerarHash(string senha)
    {
      using (var algorithm = new Rfc2898DeriveBytes(
        senha,
        16,
        10000,
        HashAlgorithmName.SHA512))
      {
        var key = Convert.ToBase64String(algorithm.GetBytes(32));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{salt}.{key}";
      }
    }

    public bool Validar(string hash, string senha)
    {
      var parts = hash.Split('.', 2);

      var salt = Convert.FromBase64String(parts[0]);
      var key = Convert.FromBase64String(parts[1]);

      using (var algorithm = new Rfc2898DeriveBytes(
        senha,
        salt,
        10000,
        HashAlgorithmName.SHA512))
      {
        var keyToCheck = algorithm.GetBytes(32);

        var verificar = keyToCheck.SequenceEqual(key);

        return verificar;
      }
    }
  }
}