using Blockchain.Apps;
using Blockchain.Helpers;

var app = new CoinApp();
var user1 = app.GenerateKeys();
var user2 = app.GenerateKeys();

app.PerformTransaction(user1, user2.PublicKey, 50);
app.PerformTransaction(user2, user1.PublicKey, 105);
app.PerformTransaction(user2, user1.PublicKey, 50);
