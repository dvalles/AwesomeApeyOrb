const express = require('express');
const admin = require('firebase-admin');
const pass = require('./password.js');
var db = admin.firestore();

/*
 * An express route for all cloud functions dealig with organization
 */

//the express app
const app = express();

const s = {
    success: "SUCCESS",
}

const e = {

}

//#endregion

//GET, POST, PUT, DELETE

//#region POST

//Post a level time
app.post('/newTime', async (req, res) => {
    //password check
    if (req.body[pass.field] != pass.pass)
        return res.status('404').send('Uhh Uhh Uhh.. you didnt say the magic word');

    //create the document info
    var nameVal = req.query.name;
    var timeVal = parseFloat(req.query.time);
    var level = req.query.level;
    var playerID = req.query.playerID;
    var info = {
        name: nameVal,
        time: timeVal,
    }

    //just in case
    if (info.time == 0)
        return res.status('404').send('Problem with sent time ' + req.query.time);

    //set the document with info
    var ref = db.collection(level).doc(playerID);
    ref.get().then(async (userDoc) => {
        if (userDoc.exists) {
            var userData = userDoc.data();
            if (info.time < userData.time) {
                await ref.set(info);
            } else if (userData.name != info.name) {
                await ref.set({ name: info.name }, { merge: true });
            }
            return res.send(s.success);
        } else {
            await ref.set(info);
            return res.send(s.success);
        }
    }).catch((err) => {
        return res.status('404').send('Problem fetching player document');
    });

});

//Get the current level rankings
app.post('/getRankings', async (req, res) => {
    //password check
    if (req.body[pass.field] != pass.pass)
        return res.status('404').send('Uhh Uhh Uhh.. you didnt say the magic word');

    //create the ref
    var level = req.query.level;
    var playerID = req.query.playerID;
    var ref = db.collection(level);
    var refUser = db.collection(level).doc(playerID);

    var promises = [];
    promises.push(ref.orderBy('time', 'asc').limit(10).get());
    promises.push(refUser.get());
    Promise.all(promises).then((values) => {
        var rankingsSnap = values[0]; //
        var userDoc = values[1];
        var rankings = {};
        var rank = 1;
        rankingsSnap.forEach(x => {
            var data = x.data();
            rankings[`rank${rank}`] = data;
            rank++;
        })
        rankings.player = userDoc.data();
        return res.send(rankings);
    }).catch((err) => {
        return res.status('404').send('Problem fetching rankings');
    });
});

exports.app = app;