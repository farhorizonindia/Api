﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoApi.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Your ASP.NET application</title>
    <style>
        body {
            background: #fff;
            color: #505050;
            font: 14px 'Segoe UI', tahoma, arial, helvetica, sans-serif;
            margin: 20px;
            padding: 0;
        }

        #header {
            background: #efefef;
            padding: 0;
        }

        h1 {
            font-size: 48px;
            font-weight: normal;
            margin: 0;
            padding: 0 30px;
            line-height: 150px;
        }

        p {
            font-size: 20px;
            color: #fff;
            background: #969696;
            padding: 0 30px;
            line-height: 50px;
        }

        #main {
            padding: 5px 30px;
        }

        .section {
            width: 21.7%;
            float: left;
            margin: 0 0 0 4%;
        }

            .section h2 {
                font-size: 13px;
                text-transform: uppercase;
                margin: 0;
                border-bottom: 1px solid silver;
                padding-bottom: 12px;
                margin-bottom: 8px;
            }

            .section.first {
                margin-left: 0;
            }

                .section.first h2 {
                    font-size: 24px;
                    text-transform: none;
                    margin-bottom: 25px;
                    border: none;
                }

                .section.first li {
                    border-top: 1px solid silver;
                    padding: 8px 0;
                }

            .section.last {
                margin-right: 0;
            }

        ul {
            list-style: none;
            padding: 0;
            margin: 0;
            line-height: 20px;
        }

        li {
            padding: 4px 0;
        }

        a {
            color: #267cb2;
            text-decoration: none;
        }

            a:hover {
                text-decoration: underline;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="header">
        <h1>Your ASP.NET application</h1>
        <p>Congratulations! You've created a project</p>
    </div>

    <div id="main">
        <div class="section first">
            <h2>This application consists of:</h2>
            <ul>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615543">Help Page</a> for documenting your Web APIs</li>
                <li>Theming using <a href="http://go.microsoft.com/fwlink/?LinkID=615519">Bootstrap</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=320957">Authentication</a>, if selected, shows how to register and sign in</li>
                <li>ASP.NET features managed using <a href="http://go.microsoft.com/fwlink/?LinkID=320958">NuGet</a></li>
            </ul>
        </div>

        <div class="section">
            <h2>Customize app</h2>
            <ul>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=320959">Get started with HTTP services using ASP.NET Web API</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=320960">Change the site's theme</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=320961">Add more libraries using NuGet</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=320962">Configure authentication</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=320963">Scaffold an ASP.NET Web API from a model</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615545">Secure your web API</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615544">Access your web API on different devices</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615546">Enable tracing for testing and debugging</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615530">Add real-time web with ASP.NET SignalR</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615531">Add components using Scaffolding</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615533">Share your project</a></li>
            </ul>
        </div>

        <div class="section">
            <h2>Deploy</h2>
            <ul>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615534">Ensure your app is ready for production</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615535">Microsoft Azure</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615536">Hosting providers</a></li>
            </ul>
        </div>

        <div class="section last">
            <h2>Get help</h2>
            <ul>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615537">Get help</a></li>
                <li><a href="http://go.microsoft.com/fwlink/?LinkID=615538">Get more templates</a></li>
            </ul>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
