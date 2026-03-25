const express = require("express");
const app = express();
app.use(express.json());

const users = {}; // in-memory user storage

// REGISTER NEW USER
app.post("/warriors/register", (req, res) => {
    const auid = "AUID-" + Math.random().toString(36).substring(2);
    const gameUserID = "USER-" + Math.random().toString(36).substring(2);
    const sessionToken = "TOKEN-" + Math.random().toString(36).substring(2);

    users[auid] = {
        gameUserID,
        sessionToken,
        save: {}
    };

    res.json({
        sessionToken,
        gameUserID,
        AUID: auid
    });
});

// LOGIN EXISTING USER
app.post("/battle-braves/login", (req, res) => {
    const { AUID } = req.body;

    if (!AUID || !users[AUID]) {
        return res.status(400).json({ error: "Unknown AUID" });
    }

    const user = users[AUID];

    res.json({
        sessionToken: user.sessionToken,
        gameUserID: user.gameUserID,
        deviceTokenHash: "hash123"
    });
});

// SAVE GAME
app.post("/warriors/save-game", (req, res) => {
    const { AUID, data } = req.body;

    if (AUID && users[AUID]) {
        users[AUID].save = data;
    }

    res.json({ success: true });
});

// ROOT
app.get("/", (req, res) => {
    res.send("World of Warriors private server running");
});

const port = process.env.PORT || 3000;
app.listen(port, () => console.log("Server running on port " + port));
