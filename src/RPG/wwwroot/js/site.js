function hideAllPanes() {
    $("#dashboard").fadeOut(50);
    $("#research").fadeOut(50);
    $("#humanResources").fadeOut(50);
}

function showPane(paneName) {
    hideAllPanes();
    $("#" + paneName).fadeIn(50);
}

function showResearch() {
    console.log("Showing research");
    showPane("research");
}

function showHumanResources() {
    console.log("Showing human resources");
    showPane("humanResources");
}

function showDashboard() {
    console.log("showing dashboard");
    showPane("dashboard");
}

function initView(data) {
    console.log("initializing view");
    showDashboard();
    $("#outputBox").html(data.Messages);
}

function refreshViews() {
    window.location.reload();
}


function addSingleAction(script, sender) {
    clientModel.Actions += script;
    console.log("Added single action to script: " + script);
    sender.onclick = function() {console.log("Cannot repeat this action!")};
}

function endTurn(url) {
    console.log("Ending turn...");
    console.log("Sending: " + JSON.stringify(clientModel));
    $.post(url, clientModel, function (data) {
        endTurnCallback(data);
    });
}

function endTurnCallback(data) {
    console.log("New ActionModel returned. Model: " + JSON.stringify(data));
    console.log("Messages returned: " + data.Messages);
    refreshViews();
}
                    
                
            
        
    