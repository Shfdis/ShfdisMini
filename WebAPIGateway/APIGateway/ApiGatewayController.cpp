#include "ApiGatewayController.h"
#include <cstring>
#include <drogon/HttpClient.h>
#include <drogon/HttpRequest.h>
#include <drogon/HttpTypes.h>
#include <json/value.h>
#include <memory>
#include <stdexcept>
#include <trantor/utils/Logger.h>
char* GetEnv(const char *name) {
    extern char **environ;
    for (char **p = environ; *p != NULL; p++) {
        std::string cur(*p);
        size_t eqlsign = cur.find('=');
        if (cur.substr(0, eqlsign) == name) {
            return strdup(cur.substr(eqlsign + 1).c_str());
        }
    }
    // If you reach here, the environment variable was not found.
    return NULL;
}
void ApiGatewayController::Status(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    Json::Value json;
    json["status"] = "ok";
    auto resp = HttpResponse::newHttpJsonResponse(json);
    callback(resp);
}
const std::shared_ptr<Json::Value> ApiGatewayController::MakeApiRequest(std::string& address, std::string& path, Json::Value& json, drogon::HttpMethod method) {
    auto client = HttpClient::newHttpClient(address);
    auto request = HttpRequest::newHttpRequest();
    request->setMethod(method);
    request->setPath(path);
    request->setBody(json.toStyledString());
    request->setContentTypeCode(CT_APPLICATION_JSON);
    std::pair<ReqResult, HttpResponsePtr> response = client->sendRequest(request);
    if (response.first == ReqResult::Ok) {
        if (response.second->getStatusCode() != 200) {
            LOG_ERROR << "SendMail error: " << response.second->getBody() << '\n';
            throw new std::runtime_error("SendMail error");
        }
        return response.second->getJsonObject();
    }
    throw new std::runtime_error("SendMail error");
}
void ApiGatewayController::SendMail(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("MAIL_CONFIRMATION_HOST");
    std::string path = "/mail";
    LOG_COMPACT_DEBUG << "SendMail address: " << address << " path: " << path << '\n';
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "SendMail error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}
void ApiGatewayController::ConfirmMail(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("MAIL_CONFIRMATION_HOST");
    std::string path = "/mail/confirm";
    LOG_COMPACT_DEBUG << "ConfirmMail address: " << address << " path: " << path << '\n';
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "ConfirmMail error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}
void ApiGatewayController::AddUser(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("LOGINMANAGER_HOST");
    std::string path = "/signup";
    LOG_COMPACT_DEBUG << "AddUser address: " << address << " path: " << path << '\n';
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "AddUser error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}
void ApiGatewayController::RemoveUser(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("LOGINMANAGER_HOST");
    std::string path = "/delete";
    LOG_COMPACT_DEBUG << "RemoveUser address: " << address << " path: " << path << '\n';
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "RemoveUser error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}
void ApiGatewayController::LoginUser(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("LOGINMANAGER_HOST");
    std::string path = "/session";
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "LoginUser error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}

void ApiGatewayController::SessionIsActive(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("LOGINMANAGER_HOST");
    std::string path = "/session/" + req->getParameter("token");
    LOG_COMPACT_DEBUG << "SessionIsActive address: " << address << " path: " << path << '\n';
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "SessionIsActive error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}
void ApiGatewayController::EndSession(const HttpRequestPtr &req, std::function<void(const HttpResponsePtr &)> &&callback) {
    std::string address = GetEnv("LOGINMANAGER_HOST");
    std::string path = "/session/" + req->getParameter("token");
    LOG_COMPACT_DEBUG << "EndSession address: " << address << " path: " << path << '\n';
    try {
        auto json = MakeApiRequest(address, path, *req->getJsonObject(), req->getMethod());
        auto resp = HttpResponse::newHttpJsonResponse(json->toStyledString());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        callback(resp);
    }
    catch (std::exception& e) {
        LOG_ERROR << "EndSession error: " << e.what() << '\n';
        auto resp = HttpResponse::newHttpJsonResponse(e.what());
        resp->setContentTypeCode(CT_APPLICATION_JSON);
        resp->setStatusCode(HttpStatusCode(500));
        callback(resp);
    }
}