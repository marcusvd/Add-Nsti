

                if (user != null && !await _userManager.IsLockedOutAsync(user))
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        if (!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "E-mail não está Válido!");
                            return View();
                        }

                        await _userManager.ResetAccessFailedCountAsync(user);

                        if (await _userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validator = await _userManager.GetValidTwoFactorProvidersAsync(user);

                            if (validator.Contains("Email"))
                            {
                                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                                System.IO.File.WriteAllText("email2sv.txt", token);

                                // Gerar JWT para two-factor em vez de usar cookie
                                var jwtToken = GenerateTwoFactorJwtToken(user.Id, "Email");
                                
                                return RedirectToAction("TwoFactor", new { token = jwtToken });
                            }
                        }

                        // Login direto sem two-factor - gerar JWT principal
                        var authToken = await GenerateJwtToken(user);
                        return Ok(new { Token = authToken, RedirectUrl = Url.Action("About") });
                    }

                    await _userManager.AccessFailedAsync(user);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        // Email deve ser enviando com sugestão de Mudança de Senha!
                    }
                }

                ModelState.AddModelError("", "Usuário ou Senha Invalida");
            }

            return View();


                if (user != null && !await _userManager.IsLockedOutAsync(user))
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        if (!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "E-mail não está Válido!");
                            return View();
                        }

                        await _userManager.ResetAccessFailedCountAsync(user);

                        if(await _userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validator = await _userManager.GetValidTwoFactorProvidersAsync(user);

                            if (validator.Contains("Email"))
                            {
                                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                                System.IO.File.WriteAllText("email2sv.txt", token);

                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                                    Store2FA(user.Id, "Email"));

                                return RedirectToAction("TwoFactor");
                            }
                        }

                        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

                        return RedirectToAction("About");
                    }                    

                    await _userManager.AccessFailedAsync(user);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        //Email deve ser enviando com sugestão de Mudança de Senha!
                    }
                }

                ModelState.AddModelError("", "Usuário ou Senha Invalida");
            }

            return View();
        }