<%@ Page Title="404 Oops! This Page Could Not Be Found - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #notfound {
            position: relative;
            top: 150px;
        }

            #notfound .notfound {
                position: absolute;
                left: 50%;
                top: 50%;
                -webkit-transform: translate(-50%, -50%);
                -ms-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);
            }

        .notfound {
            max-width: 767px;
            width: 100%;
            line-height: 1.4;
            padding: 0px 15px;
        }

            .notfound .notfound-404 {
                position: relative;
                height: 150px;
                line-height: 150px;
                margin-bottom: 60px;
            }

                .notfound .notfound-404 h1 {
                    font-family: 'Titillium Web', sans-serif;
                    font-size: 186px;
                    font-weight: 900;
                    margin: 0px;
                    text-transform: uppercase;
                    background: url('/assets/images/error_back.png');
                    background-position-x: 0%;
                    background-position-y: 0%;
                    background-size: auto;
                    background-clip: border-box;
                    -webkit-background-clip: text;
                    -webkit-text-fill-color: transparent;
                    background-size: cover;
                    background-position: center;
                }

            .notfound h2 {
                font-family: 'Titillium Web', sans-serif;
                font-size: 26px;
                font-weight: 700;
                margin: 0;
            }

            .notfound p {
                font-family: 'Montserrat', sans-serif;
                font-size: 14px;
                font-weight: 500;
                margin-bottom: 0px;
                /*text-transform: uppercase;*/
                line-height: 25px;
            }

            .notfound a {
                font-family: 'Titillium Web', sans-serif;
                display: inline-block;
                text-transform: uppercase;
                color: #fff;
                text-decoration: none;
                border: none;
                background: #5c91fe;
                padding: 10px 40px;
                font-size: 14px;
                font-weight: 700;
                border-radius: 1px;
                margin-top: 15px;
                -webkit-transition: 0.2s all;
                transition: 0.2s all;
                background-color: #0bbc56;
            }

                .notfound a:hover {
                    color: #fff;
                }

        @media only screen and (max-width:765px) {
            .notfound .notfound-404 h1 {
                font-size: 120px;
            }

            .notfound .notfound-404 {
                margin-bottom: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="notfound">
        <div class="notfound">
            <div class="notfound-404">
                <h1>404</h1>
            </div>
            <h2>Oops! This Page Could Not Be Found</h2>
            <p>Sorry but the page you are looking for does not exist, have been removed, name changed, you don't have rights to view or is temporarily unavailable</p>
            <a href="/profile/settings/">Go To your settings</a>
        </div>
    </div>
</asp:Content>

