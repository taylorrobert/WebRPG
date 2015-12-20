function hideAllPanes() {
    $("#dashboard").fadeOut(500);
    $("#research").fadeOut(500);
}

function showPane(paneName) {
    hideAllPanes();
    $("#" + paneName).fadeIn(1000);
}

function showResearch() {
    console.log("Showing research");
    showPane("research");
}

function showDashboard() {
    console.log("showing dashboard");
    showPane("dashboard");
}

function initView() {
    console.log("initializing view");
    $("#research").hide();
}


function addSingleAction(script, sender) {
    clientModel.Actions += script;
    console.log("Added single action to script: " + script);
    sender.onclick = function() {console.log("Cannot repeat this action!")};
}

function endTurn(url) {
    console.log("Ending turn...");
    console.log("Sending: " + clientModel);
    $.post(url, clientModel, function (data) {
        endTurnCallback(data);
    });
}
          

function endTurnCallback(data) {
    console.log("New ActionModel returned. Model: " + JSON.stringify(data));
}
                    
                
            
        
    