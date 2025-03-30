#include <drogon/HttpAppFramework.h>
#include <drogon/drogon.h>
using namespace drogon;
int main() {
    app().loadConfigFile("./config.json");
    using Callback = std::function<void (const HttpResponsePtr &)> ;

    app().registerHandler("/", [](const HttpRequestPtr& req, Callback &&callback)
    {
        auto resp = HttpResponse::newHttpResponse();
        resp->setBody("Hello World");
        callback(resp);
    });
    app().run();
    
    return 0;
}