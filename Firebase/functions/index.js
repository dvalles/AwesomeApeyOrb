const functions = require("firebase-functions");
const admin = require('firebase-admin');

var serviceAccount = require("./serviceAccountKey.json");
admin.initializeApp({
    credential: admin.credential.cert(serviceAccount),
    // databaseURL: ""
});

//______________USERAUTH_EVENTS______________
const leaderboardFunctions = require('./LeaderboardFunctions.js');

exports.leaderboardFunctions = functions.https.onRequest(leaderboardFunctions.app);