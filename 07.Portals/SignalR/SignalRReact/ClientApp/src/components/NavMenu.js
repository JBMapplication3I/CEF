import React, { useState } from "react";
import { Link } from "react-router-dom";
//import { MicroCart } from "../Cart/views";
//import { Categories } from "../_shared/Categories";
//import { MiniMenu } from "../_shared/MiniMenu";
//import LogoImg from "../_meta/images/clarity-ecommerce-logo.png";
//import { Search } from "../Catalog/controls/Search";

export const NavMenu = () => {

    return (
        <header className="header container-fluid d-print-none px-0" id="header">
            <nav
                className="align-items-stretch navbar
          navbar-expand-lg navbar-light d-flex flex-column p-0"
                role="navigation">
                <div id="headerMid" className="bg-light">
                    <div className="row w-100">
                        <div className="col-12 xs-text-center sm-text-center col-md-auto">
                            <Link to="/">
                                &nbsp;
                                {/*<img*/}
                                {/*    className="img-fluid lazyloaded"*/}
                                {/*    src={LogoImg}*/}
                                {/*    alt="Clarity Ventures Inc"*/}
                                {/*/>*/}
                            </Link>
                        </div>
                        <div className="col form-inline">
                            {/*<Search />*/}
                        </div>
                        <div className="col-12 col-xl-auto" style={{ minHeight: "70px" }}>
                            <div className="row align-items-center h-100">
                                {/*<MicroCart />*/}
                                {/*<MiniMenu />*/}
                            </div>
                        </div>
                    </div>
                    <div className="header-content d-flex align-items-center">
                        <button
                            type="button"
                            className="navbar-toggler ml-2"
                            data-toggle="collapse"
                            data-target="#navbarSupportedContent"
                            aria-controls="navbarSupportedContent"
                            aria-expanded="false"
                            aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                    </div>
                </div>
                <div className="header-navigate navbar-dark bg-dark w-100">
                    <div className="container">
                        <div
                            className="collapse navbar-collapse d-lg-flex align-items-center"
                            id="navbarSupportedContent">
                            <ul className="navbar-nav">
                                <li className="nav-item dropdown mega-dropdown position-static">
                                    <Link
                                        className="nav-link dropdown-toggle"
                                        to="/catalog"
                                        data-bs-toggle="dropdown"
                                        aria-expanded="false">
                                        Products
                                    </Link>
                                    <div className="dropdown-menu mega-menu w-100 top-auto">
                                        {/*<Categories />*/}
                                    </div>
                                </li>
                                <li className="nav-item dropdown">
                                    <Link
                                        className="nav-link dropdown-toggle"
                                        id="dropdownMenu"
                                        to="/about"
                                        data-toggle="dropdown"
                                        role="button"
                                        aria-haspopup="true"
                                        aria-expanded="false">
                                        About
                                    </Link>
                                    <ul className="dropdown-menu" aria-labelledby="dropdownMenu">
                                        <li>
                                            <Link className="dropdown-item" to="/">
                                                Link 1
                                            </Link>
                                        </li>
                                        <li>
                                            <Link className="dropdown-item" to="/">
                                                Link 2
                                            </Link>
                                        </li>
                                        <li>
                                            <Link className="dropdown-item" to="/">
                                                Link 3
                                            </Link>
                                        </li>
                                    </ul>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link" to="/news">
                                        News
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link" to="/industry">
                                        Industry
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link" to="/request-info">
                                        Request Info
                                    </Link>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>
        </header>
    );
};
