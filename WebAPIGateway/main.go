package main

import (
	"io"
	"net/http"
	"os"
	"time"

	"github.com/gin-gonic/gin"
)

// Configure HTTP client with timeout
var httpClient = &http.Client{
	Timeout: 30 * time.Second,
}

func main() {
	mailHost := os.Getenv("MAIL_HOST")
	loginHost := os.Getenv("LOGIN_HOST")

	router := gin.Default()

	router.GET("/", helloWorld)
	router.POST("/mail", forwardPost(mailHost+"/mail"))
	router.PUT("/mail/confirm", forwardPut(mailHost+"/mail/confirm"))
	router.POST("/signup", forwardPost(loginHost+"/signup"))
	router.DELETE("/user/delete", forwardDelete(loginHost+"/delete"))
	router.POST("/session", forwardPost(loginHost+"/session"))
	router.GET("/session/:token", forwardGetWithToken(loginHost+"/session"))
	router.DELETE("/session/:token", forwardDeleteWithToken(loginHost+"/session"))

	router.Run("0.0.0.0:8080")
}

func helloWorld(c *gin.Context) {
	c.IndentedJSON(http.StatusOK, gin.H{"status": "ok"})
}

// Helper to proxy responses
func proxyResponse(c *gin.Context, resp *http.Response, err error) {
	if err != nil {
		c.JSON(http.StatusBadGateway, gin.H{
			"error":   "Bad Gateway",
			"message": err.Error(),
		})
		return
	}
	defer resp.Body.Close()

	// Copy headers
	for key, values := range resp.Header {
		for _, value := range values {
			c.Writer.Header().Add(key, value)
		}
	}

	// Copy status code
	c.Status(resp.StatusCode)

	// Copy body
	_, err = io.Copy(c.Writer, resp.Body)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{
			"error":   "Response Copy Failed",
			"message": err.Error(),
		})
	}
}

// Handler factories for different methods
func forwardPost(target string) gin.HandlerFunc {
	return func(c *gin.Context) {
		resp, err := httpClient.Post(target, "application/json", c.Request.Body)
		proxyResponse(c, resp, err)
	}
}

func forwardPut(target string) gin.HandlerFunc {
	return func(c *gin.Context) {
		req, err := http.NewRequest(http.MethodPut, target, c.Request.Body)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}
		req.Header = c.Request.Header
		resp, err := httpClient.Do(req)
		proxyResponse(c, resp, err)
	}
}

func forwardDelete(target string) gin.HandlerFunc {
	return func(c *gin.Context) {
		req, err := http.NewRequest(http.MethodDelete, target, c.Request.Body)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}
		req.Header = c.Request.Header
		resp, err := httpClient.Do(req)
		proxyResponse(c, resp, err)
	}
}

func forwardGetWithToken(basePath string) gin.HandlerFunc {
	return func(c *gin.Context) {
		token := c.Param("token")
		url := basePath + "/" + token // Fixed path construction
		resp, err := httpClient.Get(url)
		proxyResponse(c, resp, err)
	}
}

func forwardDeleteWithToken(basePath string) gin.HandlerFunc {
	return func(c *gin.Context) {
		token := c.Param("token")
		url := basePath + "/" + token
		req, err := http.NewRequest(http.MethodDelete, url, c.Request.Body)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}
		req.Header = c.Request.Header
		resp, err := httpClient.Do(req)
		proxyResponse(c, resp, err)
	}
}
