#pragma once
#include <drogon/HttpController.h>
#include <drogon/HttpTypes.h>
#include <json/value.h>

using namespace drogon;

class ApiGatewayController : public drogon::HttpController<ApiGatewayController, false> {
    public:
        METHOD_LIST_BEGIN
            ADD_METHOD_TO(ApiGatewayController::Status, "/", drogon::Get);
            ADD_METHOD_TO(ApiGatewayController::SendMail, "/mail/send", drogon::Post);
            ADD_METHOD_TO(ApiGatewayController::ConfirmMail, "/mail/confirm", drogon::Put);
            ADD_METHOD_TO(ApiGatewayController::AddUser, "/user/signup", drogon::Post);
            ADD_METHOD_TO(ApiGatewayController::RemoveUser, "/user", drogon::Delete);
            ADD_METHOD_TO(ApiGatewayController::LoginUser, "/user/login", drogon::Put);
            ADD_METHOD_TO(ApiGatewayController::SessionIsActive, "/session/is_active", drogon::Post);
            ADD_METHOD_TO(ApiGatewayController::EndSession, "/session", drogon::Delete);
        METHOD_LIST_END
    private:
        const std::shared_ptr<Json::Value>  MakeApiRequest(std::string& address, std::string& path, Json::Value& json, drogon::HttpMethod method);
        void Status(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void ConfirmMail(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void SendMail(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void AddUser(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void RemoveUser(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void LoginUser(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void SessionIsActive(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
        void EndSession(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback);
};