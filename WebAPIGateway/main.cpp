#include <drogon/drogon.h>
#include <memory>
#include "APIGateway/ApiGatewayController.h"
using namespace std;
using namespace drogon;

int main() {
    const auto port(8080);

    app()
        .setLogPath("./")
        .setLogLevel(trantor::Logger::kDebug)
        .addListener("0.0.0.0", port)
        .setThreadNum(16);

    auto controller(make_shared<ApiGatewayController>());
    app().registerController(controller);
    app().run();

    return 0;
}